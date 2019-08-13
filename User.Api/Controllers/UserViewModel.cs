namespace User.Api.Controllers
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }        
        public string Password { get; set; }

        public UserViewModel(){}
        public UserViewModel(Domain.User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            Surname = user.Surname;
            Email = user.Email;
            Password = user.Password;
        }
    }
}