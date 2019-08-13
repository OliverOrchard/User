using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using static System.String;

namespace User.Domain
{
    public class User
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "surname")]
        public string Surname { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        public static User Create(string id, string password, string email, string firstName, string surname)
        {
            if (IsNullOrEmpty(email) || !new EmailAddressAttribute().IsValid(email))
            {
                throw new Exception("Invalid Email");   
            }

            return new User()
            {
                Id = id,
                Password = password,
                Email = email,
                FirstName = firstName,
                Surname = surname
            };
        }

        public void Update(User user)
        {
            Password = user.Password;
            Email = user.Email;
            FirstName = user.FirstName;
            Surname = user.Surname;
        }
    }
}
