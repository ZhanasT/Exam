using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.FileProviders;
using System.Text;
using CinemaAPI.Models;
using CinemaDB;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.ISSUER,
            ValidateAudience = true,
            ValidAudience = AuthOptions.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.FromSeconds(500)
        };
    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDirectoryBrowser();

var app = builder.Build();

app.MapPost("/security/createToken", (UserModel userModel) => {
    var cinemaRepo = new Repository();
    if (userModel.Username is null || userModel.Password is null)
        return Results.BadRequest();
    User? user = cinemaRepo.SearchUser(userModel.Username, userModel.Password);
    if (user is null)
        return Results.Unauthorized();
    
    var claims = new List<Claim> {new Claim(ClaimTypes.Name, user.Login)};

    var jwt = new JwtSecurityToken(
        issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
        claims: claims,
        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
    
    var response = new
    {
        access_token = encodedJwt,
        login = user.Login
    };

    return Results.Json(response);
});

app.MapPost("/security/register", (UserModel userModel) => {
    var cinemaRepo = new Repository();
    if (userModel.Username is not null && userModel.Password is not null)
    {
        if (cinemaRepo.SearchUser(userModel.Username, userModel.Password) is null)
        {
            cinemaRepo.AddUser(userModel.Username, userModel.Password);
            return Results.Ok();
        }
        else
        {
            return Results.BadRequest();
        }
        
    }
    else
    {
        return Results.BadRequest();
    }
});

app.Map("/security/data", [Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)] () => new {  });
app.UseCors(x =>  x.AllowAnyHeader().AllowAnyMethod().WithOrigins());

app.UseAuthorization();
app.UseAuthentication();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot/html/")),
    RequestPath = "/index.html",
});

app.UseAuthorization();

app.MapControllers();

app.Run();
