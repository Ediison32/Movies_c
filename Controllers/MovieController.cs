using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using moviesSystem.Models;
using moviesSystem.Data; 
namespace moviesSystem.Controllers;

public class MovieController : Controller
{
    // instanciar la db 
    
    private readonly AppDbContext _context;
    public MovieController(AppDbContext context)
    {
        _context = context;
    }
    // agregar una pelicual 
    public async Task<IActionResult> Index()
    {
        var rdm = new Random();
        List<Movie> pelis = new List<Movie>();
        HttpClient client = new HttpClient();
        for (int i = 0; i < 10; i++)
        {
            char letter = (char)rdm.Next('A', 'Z' + 1);
            char secondletter = (char)rdm.Next('A', 'Z' + 1);
            string apiurl = $"https://www.omdbapi.com/?t={letter}{secondletter}&apikey=9fe20006";
            var respuesta = await client.GetStringAsync(apiurl);
            Movie? peli = JsonSerializer.Deserialize<Movie>(respuesta);
            pelis.Add(peli);
        }
        return View(pelis);
    }
    
    
    public async Task<IActionResult> ListFavorites()
    {
        var favorites = await _context.movies.ToListAsync();
        return View("Index", favorites);
    }
    
    public async Task<IActionResult> AddMovie(Movie movie)
    {
        await _context.movies.AddRangeAsync(movie);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}