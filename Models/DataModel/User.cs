using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace DotnetFlix.Models
{
    public class User: CreateUpdate
    {
        [Key]
        public int UserId { get; set; }

       [Display(Name="First Name")]
       [Required(ErrorMessage="First Name is required.")]
       [MinLength(3, ErrorMessage="First Name should have a min {1}")]
       [MaxLength(50, ErrorMessage="First Name should have a max {1}")]       
       public string FirstName { get; set; }
       
       [Display(Name="Last Name")]
       [Required(ErrorMessage="Last Name is required.")]
       [MinLength(3, ErrorMessage="Last Name should have a min {1}")]
       [MaxLength(50, ErrorMessage="Last Name should have a max {1}")] 
       public string LastName { get; set; }
       
       [Display(Name="Email")]
       [Required(ErrorMessage="Email is required")]
       [DataType(DataType.EmailAddress)]
       [EmailAddress]
       public string Email { get; set; }
       
       [Display(Name="Password")]
       [Required(ErrorMessage="Password is required")]
       [MinLength(8, ErrorMessage="Password should have a min {1}")]
    //    [MaxLength(20, ErrorMessage="Email should have a max {1}")] 
       [DataType(DataType.Password)]
       public string Password { get; set; }

       [NotMapped]
       [Compare("Password", ErrorMessage="Password do not match.")]
       [DataType(DataType.Password)]
       [Display(Name="Confirm Password")]
       public string Confirm {get; set; }

       public string UserType { get; set; } = "Normal";

        public List<UserMovie> UserMovies { get; set; } = new List<UserMovie>();
    }
}