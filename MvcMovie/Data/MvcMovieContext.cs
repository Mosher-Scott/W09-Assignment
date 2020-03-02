using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

/// <summary>
/// We will use this for EF Core functionality.  It will specify the table to be used for the data model. 
/// For this to work, you will need to register the database context within the appsettings.json file and Startup.cs
/// </summary>
namespace MvcMovie.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options): base(options)
        {

        }

        // Create a DbSet property for the Movie table.  Per the instructions online: The preceding code creates a DbSet<Movie> property for the entity set. In Entity Framework terminology, an entity set typically corresponds to a database table. An entity corresponds to a row in the table.
        public DbSet<Movie> Movie { get; set; }
    }
}
