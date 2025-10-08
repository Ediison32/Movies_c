using System.ComponentModel.DataAnnotations;

namespace MovieSistem.Models;

public class Movie
{
    [Key]
    public string imdbID { get; set; } = string.Empty;
    
    public string Title { get; set; } = string.Empty;
    
    public DateOnly Year { get; set; }
    
    public string Genre { get; set; } = string.Empty;
    
    public string Poster { get; set; } = string.Empty;
    
    public string Plot { get; set; } = string.Empty;
    
}