using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace User.Tests
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void Create_MapsFieldsCorrectly()
        {
            var id = "3fd8cb2c-45a2-4e91-a56f-808c53234f32";
            var email = "test@email.com";
            var firstName = "Edward";
            var surname = "Bernays";
            var password = "password1";

            var user = Domain.User.Create(id, password, email, firstName, surname);

            user.Id.Should().Be(id);
            user.Email.Should().Be(email);
            user.FirstName.Should().Be(firstName);
            user.Surname.Should().Be(surname);
            user.Password.Should().Be(password);
        }

        [TestMethod]
        public void Update_PasswordUpdated()
        {
            var id = "3fd8cb2c-45a2-4e91-a56f-808c53234f32";
            var email = "test@email.com";
            var firstName = "Edward";
            var surname = "Bernays";
            var password = "password1";
            var user = new Domain.User()
            {
                Id = id,
                Password = password,
                Email = email,
                FirstName = firstName,
                Surname = surname
            };

            var newPassword = "password2";
            user.Update(newPassword, email, firstName, surname);

            user.Id.Should().Be(id);
            user.Email.Should().Be(email);
            user.FirstName.Should().Be(firstName);
            user.Surname.Should().Be(surname);
            user.Password.Should().Be(newPassword);
        }

        [TestMethod]
        public void Update_EmailUpdated()
        {
            var id = "3fd8cb2c-45a2-4e91-a56f-808c53234f32";
            var email = "test@email.com";
            var firstName = "Edward";
            var surname = "Bernays";
            var password = "password1";
            var user = new Domain.User()
            {
                Id = id,
                Password = password,
                Email = email,
                FirstName = firstName,
                Surname = surname
            };

            var newEmail = "edward.bernays@email.com";
            user.Update(password, newEmail, firstName, surname);

            user.Id.Should().Be(id);
            user.Email.Should().Be(newEmail);
            user.FirstName.Should().Be(firstName);
            user.Surname.Should().Be(surname);
            user.Password.Should().Be(password);
        }

        [TestMethod]
        public void Update_FirstNameUpdated()
        {
            var id = "3fd8cb2c-45a2-4e91-a56f-808c53234f32";
            var email = "test@email.com";
            var firstName = "Edward";
            var surname = "Bernays";
            var password = "password1";
            var user = new Domain.User()
            {
                Id = id,
                Password = password,
                Email = email,
                FirstName = firstName,
                Surname = surname
            };

            var newFirstName = "edwardinho";
            user.Update(password, email, newFirstName, surname);

            user.Id.Should().Be(id);
            user.Email.Should().Be(email);
            user.FirstName.Should().Be(newFirstName);
            user.Surname.Should().Be(surname);
            user.Password.Should().Be(password);
        }

        [TestMethod]
        public void Update_SurnameUpdated()
        {
            var id = "3fd8cb2c-45a2-4e91-a56f-808c53234f32";
            var email = "test@email.com";
            var firstName = "Edward";
            var surname = "Bernays";
            var password = "password1";
            var user = new Domain.User()
            {
                Id = id,
                Password = password,
                Email = email,
                FirstName = firstName,
                Surname = surname
            };

            var newSurname = "edwardinho";

            user.Update(password, email, firstName, newSurname);

            user.Id.Should().Be(id);
            user.Email.Should().Be(email);
            user.FirstName.Should().Be(firstName);
            user.Surname.Should().Be(newSurname);
            user.Password.Should().Be(password);
        }
    }
}
