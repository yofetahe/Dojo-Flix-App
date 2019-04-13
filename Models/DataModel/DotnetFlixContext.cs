using Microsoft.EntityFrameworkCore;
 
namespace DotnetFlix.Models
{
    public class DotnetFlixContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public DotnetFlixContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }
    }
}
