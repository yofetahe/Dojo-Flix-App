using System;
using System.ComponentModel.DataAnnotations;

namespace DotnetFlix.Models
{
    public class Rating : CreateUpdate
    {
        [Display(Name="Your Rating")]
        [Required]        
        public int RatingValue { get; set; }
    }
}