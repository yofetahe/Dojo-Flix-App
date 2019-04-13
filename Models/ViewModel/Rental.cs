using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
 
namespace DotnetFlix.Models
{
    public class Rental
    {
        public Movie Movie { get; set; }
        public List<Movie> Movies { get; set; }
        public Rating Rating { get; set; }
        public UserMovie UserMovie { get; set; }
    }
}