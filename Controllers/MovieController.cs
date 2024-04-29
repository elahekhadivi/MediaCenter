using System.Net.Http.Json;
using System.Text.Json.Serialization;
using MediaCenter.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly MediaCenterDbContext _DbContext;
    private readonly HttpClient _imdbClient;

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


    [HttpGet("AddviaIMDB")]
    public IActionResult AddviaIMDB(){
        
        var _imdbClient = new HttpClient();
        HttpResponseMessage response = _imdbClient.GetAsync("https://api.themoviedb.org/3/trending/all/week?api_key=573f15303b4c2ff34b2491b6cd89c012").Result;
        if(response.IsSuccessStatusCode){
            string data = response.Content.ReadAsStringAsync().Result;
            dynamic imdbResultsObj = JsonConvert.DeserializeObject(data);
            var rnd = new Random();
            int rndMovieIndex = rnd.Next(0,imdbResultsObj["results"].Count-1);
            var movie = new Movie();

            movie.Title = imdbResultsObj["results"][rndMovieIndex]["title"];
            movie.ReleaseDate = imdbResultsObj["results"][rndMovieIndex]["release_date"];
            string Description = imdbResultsObj["results"][rndMovieIndex]["overview"];
            movie.Description = Description.Substring(0,100);
            movie.Rating = imdbResultsObj["results"][rndMovieIndex]["vote_average"];
            this.CreateUpdate(movie);
            return Ok(true);
        }
        return Ok(false);
    }

}