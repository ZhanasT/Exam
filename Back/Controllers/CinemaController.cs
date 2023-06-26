using Microsoft.AspNetCore.Mvc;
using CinemaDB;
using CinemaDB.DataModels;

namespace CinemaAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CinemaController : ControllerBase
{
    private readonly ILogger<CinemaController> _logger;
    private Repository cinemaRepo = new Repository();

    public CinemaController(ILogger<CinemaController> logger)
    {
        _logger = logger;
    }


    [HttpGet(Name = "GetTimeTableByDay")]
    public List<SessionForTimeTable> GetTimeTable(int day, int? id = null)
    {
        if (id is null)
            return cinemaRepo.GetTimeTable(day);
        else
            return cinemaRepo.GetTimeTable(day, (int)id);
    }

    [HttpGet(Name = "GetMovieInfo")]
    public MovieInfo GetMovieInfo(int id)
    {
        return cinemaRepo.GetMovieInfo(id);
    }
    [HttpGet(Name = "GetCommentsByMovie")]
    public List<CinemaDB.DataModels.Comment> GetCommentsByMovie(int id)
    {
        return cinemaRepo.GetCommentsByMovie(id);
    }
}
