using Microsoft.AspNetCore.Mvc;
using CinemaDB;

namespace CinemaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CinemaController : ControllerBase
{
    private readonly ILogger<CinemaController> _logger;

    public CinemaController(ILogger<CinemaController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetMovies")]
    public async Task<List<Session>> Get()
    {
        var cinemaRepo = new CinemaDB.Repository();
        return await cinemaRepo.GetMovies();
    }
}
