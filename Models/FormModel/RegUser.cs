using System.ComponentModel.DataAnnotations;

namespace DotnetFlix.Models
{
    public class RegUser 
    {
       [Display(Name="First Name")]
       [Required(ErrorMessage="First name is required.")]
       [MinLength(4, ErrorMessage="Min of 4 character")]
       public string FirstName { get; set; }
       
       [Display(Name="Last Name")]
       [Required(ErrorMessage="Last name is required.")]
       [MinLength(4, ErrorMessage="Min of 4 character")]
       public string LastName { get; set; }
       
       [Display(Name="Email")]
       [Required(ErrorMessage="Email is required.")]
       [EmailAddress]
       public string RegEmail { get; set; }
       
       [Display(Name="Password")]
       [Required(ErrorMessage="Password is required.")]
       [MinLength(8, ErrorMessage="Min of 8 character")]
       [DataType(DataType.Password)]
       public string RegPassword { get; set; }

       [Display(Name="Confirm Password")]
       [Compare("RegPassword", ErrorMessage="Password do not match.")]
       [DataType(DataType.Password)]
       public string RegConfirm {get; set; }
    }
}