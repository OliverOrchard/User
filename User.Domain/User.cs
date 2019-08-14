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
            return new User()
            {
                Id = id,
                Password = password,
                Email = email,
                FirstName = firstName,
                Surname = surname
            };
        }

        public void Update(string password, string email, string firstName, string surname)
        {
            Password = password;
            Email = email;
            FirstName = firstName;
            Surname = surname;
        }
    }
}
