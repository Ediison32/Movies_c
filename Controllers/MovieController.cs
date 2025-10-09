using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using moviesSystem.Data;
using moviesSystem.Models;

namespace MoviesSystem.Controllers;

public class MovieController : Controller
{
    // instanciar la db 
    
    private readonly AppDbContext _context;
    public MovieController(AppDbContext context)
    {
        _context = context;
    }
    
    // buscar por titulo 
    public async Task<IActionResult> GetByTitle(string title)
    {
        var ApiUrl = $"https://www.omdbapi.com/?s={title}&apikey=9fe20006";

        var data = new HttpClient();
        
        var respuesta = await data.GetStringAsync(ApiUrl);

        var Movies =  JsonSerializer.Deserialize<ApiResponse>(respuesta);

        return View("Index", Movies.Search);
    }
    
    
    // metod añadir a la db la informacion 
    [HttpPost]
    public async Task<IActionResult> AddMovie(Movie movie)
    {
        await _context.movies.AddRangeAsync(movie);
        await _context.SaveChangesAsync();
        return View("Index");
    }
    
    

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
    
    
    
    // guardados 
    public async Task<IActionResult> Send()
    {
        // treaer lo de la db y mostararlo en SEND 
        var favorites = await _context.movies.ToListAsync();
        return View(favorites);
    }
    
    // eliminar 
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _context.movies.FirstOrDefaultAsync(u => u.imdbID == id);
        if (user != null)
        {
            _context.movies.Remove(user);  // no necesita await porque es un metodo sincronico 
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Send");
    }
    
}