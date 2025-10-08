using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using moviesSystem.Models;
using moviesSystem.Data; 



namespace moviesSystem.Controllers;

public class MovieController
{
    // instanciar la db 
    
    private readonly AppDbContext _context;
    public MovieController(AppDbContext context)
    {
        _context = context;
    }
    // agregar una pelicual 

    public async Task<IActionConstraint> AddMovie(Movie movie)
    {
        await _context.movies.AddRangeAsync(movie);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}