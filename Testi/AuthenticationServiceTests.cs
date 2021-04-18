using BusinessLayer.Users;
using System;
using BudgetsWPF;
using Xunit;
using System.Threading.Tasks;

namespace Tests
{
    public class AuthServiceTests
    {

        [Fact]
        public void Authentication()
        {
            // Arrange
            var user1 = new RegistrationUser() { Login = "andrii2", Password = "passwor", Email = "andrii.chaliuk@ukma.edu.ua", FirstName = "Andrii", LastName = "Chaliuk" };
            var service = new AuthenticationService();
            bool registered = false;
            try
            {
                registered = Task.Run(() => service.RegisterUser(user1)).Result;
            }
            catch (Exception) { }
            var authuser1 = new AuthenticationUser() { Login = "andrii2", Password = "passwor"};
            User user = null;
            try
            {
                user = service.Authenticate(authuser1).Result;
            }
            catch (Exception) { }

            // Assert
            Assert.NotNull(user);
        }

        [Fact]
        public void RegisterUser()
        {
            // Arrange
            var user1 = new RegistrationUser() { Login = "andrii2", Password = "passwor", Email = "andrii.chaliuk@ukma.edu.ua", FirstName = "Andrii", LastName = "Chaliuk" };
            var service = new AuthenticationService();
            bool registered = false;
            try
            {
                registered = Task.Run(()=>service.RegisterUser(user1)).Result;
            }
            catch (Exception)
            {
            }

            // Act
            var actual = false;

            // Assert
            Assert.Equal(registered, actual);
        }
    }
}
