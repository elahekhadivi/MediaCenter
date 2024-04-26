using MediaCenter.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly MediaCenterDbContext _DbContext;

    public MovieController(MediaCenterDbContext dbcontext){
        this._DbContext = dbcontext;
    }

    [HttpGet("GetAll")]
    public IActionResult Get(){

        var movies = _DbContext.Movie.ToList();
        return Ok(movies);
    }

    [HttpGet("GetbyId")]
    public IActionResult GetbyId(int id){

        var movie = _DbContext.Movie.FirstOrDefault(x => x.Id == id);
        return Ok(movie);
    }


    [HttpPut(Name = "CreateUpdate")]
    public IActionResult CreateUpdate([FromBody] Movie _movie){
        
        var movie = _DbContext.Movie.FirstOrDefault(x => x.Id == _movie.Id);
        if(movie != null){
            movie.Title = _movie.Title;
            movie.Description = _movie.Description;
            _DbContext.SaveChanges();

        }else{
            _DbContext.Movie.Add(_movie);
            _DbContext.SaveChanges();
        }
        return Ok(true);

    }

    [HttpDelete("Remove/{id}")]
    public IActionResult DeletebyId(int id){
        var movie = _DbContext.Movie.FirstOrDefault(x=>x.Id == id);
        if(movie !=null){
            _DbContext.Movie.Remove(movie);
            _DbContext.SaveChanges();
            return Ok(true);
        }
        return Ok(false);
    }
}