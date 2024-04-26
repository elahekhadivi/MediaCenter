using MediaCenter.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly MediaCenterDbContext _DbContext;

    public MovieController(MediaCenterDbContext dbcontext){
        this._DbContext = dbcontext;
    }

    [HttpGet(Name = "GetAll")]
    public IActionResult Get(){

        var movies = _DbContext.Movie.ToList();
        return Ok(movies);
    }
}