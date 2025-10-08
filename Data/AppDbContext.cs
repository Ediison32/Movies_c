using Microsoft.EntityFrameworkCore;

namespace moviesSystem.Data;

using moviesSystem.Models;
public class AppDbContext : DbContext
{

    // 5 Ponemos las tablas que se van a acrear en la db 
    
    // Configuracion para traer la variable 
    public IConfiguration Configuration { get; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Movie> movies { get; set; }
    
     
    // 6 Creamos la configuracion general de la db 
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // importamos las variables  

        if (Configuration != null)
        {
            var db = Configuration.GetConnectionString("ConectionDb");

            if (!options.IsConfigured)
            {
                options.UseMySql(db, MySqlServerVersion.AutoDetect(db));
            }
        }

        // para finalizar necesitamos ejeccutar los siguientes comando para crear la migracion y para crear la db en la base de datos }
        //   dotnet ef migrations add InitialCreate
        //   dotnet ef database update
    }
}