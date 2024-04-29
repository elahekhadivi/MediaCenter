using System.Net;
using System.Runtime.CompilerServices;
using MediaCenter.Models;
using Microsoft.AspNetCore.Mvc;
using MediaCenter.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

[ApiController]
[Route("[controller]")]
public class IMDBController(IHttpClientFactory httpClientFactory) : ControllerBase
{
    private readonly string API_KEY = "573f15303b4c2ff34b2491b6cd89c012";
    private MovieController movieController;

    [HttpPut(Name ="IMDB Info")]
    public async Task<IActionResult> GetAll(){
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,$"https://api.themoviedb.org/3/trending/all/week?api_key={API_KEY}");
        var client = httpClientFactory.CreateClient();
        var response = await client.SendAsync(httpRequestMessage);
       if(response.IsSuccessStatusCode){
        var result = await response.Content.ReadAsStringAsync();
        return Ok(result);
       }
       return BadRequest();
    }
}