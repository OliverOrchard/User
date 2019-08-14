namespace User.Api.Models
{
    public class UserResponseModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }        
        public string Password { get; set; }

        public UserResponseModel(){}
        public UserResponseModel(Domain.User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            Surname = user.Surname;
            Email = user.Email;
            Password = user.Password;
        }
    }
}