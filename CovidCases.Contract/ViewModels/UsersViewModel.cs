using System.ComponentModel.DataAnnotations;
namespace CovidCases.Contract.ViewModels
{
   public class UsersViewModel
    {
        [MaxLength(20)]
        [Required(ErrorMessage = "Please enter your First name")]
        public string FirstName { get; set; }
        [MaxLength(20)]
        [Required(ErrorMessage = "Please enter your Last name")]
        public string LastName { get; set; }
    }
    public class UserInputViewModel : UsersViewModel
    {
        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter your Email address")]
        public string Email { get; set; }
        [MaxLength(20)]
        [Required(ErrorMessage = "Please enter your Password")]
        public string Password { get; set; }
    } 
}
