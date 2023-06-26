using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using CinemaDB;
using CinemaAPI.Models;

namespace CinemaAPI.Controllers;

public class AccountController
{
    private readonly ILogger<CinemaController> _logger;
    private Repository cinemaRepo = new Repository();

    public AccountController(ILogger<CinemaController> logger)
    {
        _logger = logger;
    }
    [HttpPost("Register")]
    public ActionResult Register(UserModel userModel)
    {
        cinemaRepo.AddUser(userModel.Username, userModel.Password);
        return new OkResult();
    }
    
}
