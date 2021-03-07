using BusinessLayer;
using Xunit;

namespace Tests
{
    public class UserTests
    {
        [Fact]
        public void ValidValidate()
        {
            // Arrange
            var user = new User() { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua"};
            var expected = true;

            // Act
            var actual = user.Validate();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidValidateWithoutLastName()
        {
            // Arrange
            var user = new User() { FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var expected = false;

            // Act
            var actual = user.Validate();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidValidateWithoutEmail()
        {
            // Arrange
            var user = new User() { FistName = "Andrii", LastName = "Chaliuk"};
            var expected = false;

            // Act
            var actual = user.Validate();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShareValid()
        {
            // Arrange
            var user1 = new User(1) { FistName = "Andrii", LastName = "Chaliuk", Email="andrii.chaliuk@ukma.edu.ua" };
            var user2 = new User(2) { FistName = "Apple", LastName = "Green", Email="apple.apple@apple.com" };
            var budget = new Budget(1, user1) { StartBalance = 50, Currency = Currency.EUR, Name = "Personal" };
            var expected = true;

            // Act
            var actual = user1.Share(user2, 1);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShareNotValid()
        {
            // Arrange
            var user1 = new User(1) { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var user2 = new User(2) { FistName = "Apple", LastName = "Green", Email = "apple.apple@apple.com" };
            var budget = new Budget(1, user1) { StartBalance = 50, Currency = Currency.EUR, Name = "Personal" };
            var expected = false;

            // Act
            var actual = user1.Share(user2, 2);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InstanceCount()
        {
            // Arrange
            var init = User.InstanceCount; 
            var user1 = new User(1) { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var user2 = new User(2) { FistName = "Apple", LastName = "Green", Email = "apple.apple@apple.com" };
            var user3 = new User(3) { FistName = "Orange", LastName = "Orange", Email = "orange.orange@orange.com" };
            var expected = 3;

            // Act
            var actual = User.InstanceCount - init;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
