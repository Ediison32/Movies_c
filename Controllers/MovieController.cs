using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using moviesSystem.Models;

namespace MoviesSystem.Controllers;

public class MovieController : Controller
{
    public async Task<IActionResult> GetByTitle(string title)
    {
        var ApiUrl = $"https://www.omdbapi.com/?s={title}&apikey=9fe20006";

        var data = new HttpClient();
        
        var respuesta = await data.GetStringAsync(ApiUrl);

        var Movies =  JsonSerializer.Deserialize<ApiResponse>(respuesta);
        
        return View(Movies.Search);
    }
}