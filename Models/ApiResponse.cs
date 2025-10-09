namespace moviesSystem.Models;

public class ApiResponse
{
    public List<Movie> Search { get; set; }
    public string totalResults { get; set; }
    public string Response { get; set; }
}