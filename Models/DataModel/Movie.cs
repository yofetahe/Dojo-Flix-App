using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotnetFlix.Models
{
    public class Movie : CreateUpdate
    {
        [Key]
        public int MovieId { get; set; }

        [Display(Name="Title")]
        [Required(ErrorMessage="Title is required.")]
        [MinLength(3, ErrorMessage="Title should be a min of 3 character")]
        public string Title { get; set; }

        [Display(Name="Gener")]
        [Required(ErrorMessage="Gener is required.")]
        public int GenerId { get; set; }

        [Display(Name="Released")]
        [Required(ErrorMessage="Released is required.")]
        public DateTime Released { get; set; }

        [Display(Name="Descriptioin")]
        [Required(ErrorMessage="Description is required.")]
        public string Description { get; set; }

        [Display(Name="Movie Poster")]
        [Required(ErrorMessage="Movie Poster is required")]
        public string Poster { get; set; }

        [Display(Name="Age Level")]
        [Required(ErrorMessage="Age level is required")]
        public string AgeLevel { get; set; }

        public bool Avialiablity { get; set; }

        public List<UserMovie> UserMovies { get; set; } = new List<UserMovie>();
    }
}