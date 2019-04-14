using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace DotnetFlix.Models
{
    public class User: BaseDataModel
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; } = "Normal";
        public List<UserMovie> UserMovies { get; set; } = new List<UserMovie>();

        public User() { }
        public User(RegUser form)
        {
            FirstName = form.FirstName;
            LastName = form.LastName;
            Email = form.RegEmail;
            Password = form.RegPassword;
        }
    }
}