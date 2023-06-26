using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CinemaAPI.Models;

public class AuthOptions
{
    public const string ISSUER = "CinemaAuthServer";
    public const string AUDIENCE = "CinemaAuthClient";
    const string KEY = "cinemasitesupersecretkey!56932845lskjadfblsdf3984gh3894gh";
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}
