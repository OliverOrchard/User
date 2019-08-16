using System.ComponentModel.DataAnnotations;

namespace User.Api.Models
{
    public class UserRequestModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Surname { get; set; }
        [EmailAddress, Required]
        public string Email { get; set; }
        [MinLength(7), Required]
        public string Password { get; set; }

        public UserRequestModel(){}
        public UserRequestModel(Domain.User user)
        {
            FirstName = user.FirstName;
            Surname = user.Surname;
            Email = user.Email;
            Password = user.Password;
        }
    }
}