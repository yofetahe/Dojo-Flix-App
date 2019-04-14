using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotnetFlix.Models
{
    public class Movie : BaseDataModel
    {
        [Key]
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int GenerId { get; set; }
        public DateTime Released { get; set; }
        public string Description { get; set; }
        public string Poster { get; set; }
        public string AgeLevel { get; set; }
        public bool Avialiablity { get; set; }

        public List<UserMovie> UserMovies { get; set; } = new List<UserMovie>();

        public Movie() { }
        public Movie(NewMovie form)
        {
            Title = form.Title;
            GenerId = form.GenerId;
            Released = form.Released;
            Description = form.Description;
            Poster = form.Poster;
            AgeLevel = form.AgeLevel;
            Avialiablity = true;
        }
    }
}