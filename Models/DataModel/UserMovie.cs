using System;
using System.ComponentModel.DataAnnotations;

namespace DotnetFlix.Models
{
    public class UserMovie : BaseDataModel
    {
        [Key]
        public int UserMovieId { get; set; }
        public int MovieId { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public Movie Movie { get; set; }
       
        public int RatingValue { get; set; }
        public bool isReturned { get; set; }
    }
}