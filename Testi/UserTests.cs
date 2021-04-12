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
            var user1 = new User() { FistName = "Andrii", LastName = "Chaliuk", Email="andrii.chaliuk@ukma.edu.ua" };
            var user2 = new User() { FistName = "Apple", LastName = "Green", Email="apple.apple@apple.com" };
            var budget1 = new Budget(user1, "Personal1", 50, Currency.EUR);
            var expected = true;

            // Act
            var actual = user1.Share(user2, budget1.Id);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShareNotValid()
        {
            // Arrange
            var user1 = new User() { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var user2 = new User() { FistName = "Apple", LastName = "Green", Email = "apple.apple@apple.com" };
            var budget1 = new Budget(user1, "Personal1", 50, Currency.EUR);
            var expected = false;

            // Act
            var actual = user1.Share(user2, budget1.Id + 1);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InstanceCount()
        {
            // Arrange
            var init = User.InstanceCount; 
            var user1 = new User() { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var user2 = new User() { FistName = "Apple", LastName = "Green", Email = "apple.apple@apple.com" };
            var user3 = new User() { FistName = "Orange", LastName = "Orange", Email = "orange.orange@orange.com" };
            var expected = 3;

            // Act
            var actual = User.InstanceCount - init;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DeleteBudgetCount()
        {
            // Arrange
            var user1 = new User() { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget1 = new Budget(user1, "Personal1", 50, Currency.EUR);
            var budget2 = new Budget(user1, "Personal2", 50, Currency.EUR);
            var budget3 = new Budget(user1, "Personal3", 50, Currency.EUR);
            user1.DeleteBudget(budget2.Id);
            var expected = 2;

            // Act
            var actual = user1.Budgets.Count;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidDeleteBudget()
        {
            // Arrange
            var user1 = new User() { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget1 = new Budget(user1, "Personal1", 50, Currency.EUR);
            var budget2 = new Budget(user1, "Personal2", 50, Currency.EUR);
            var budget3 = new Budget(user1, "Personal3", 50, Currency.EUR);
            var expected = true;

            // Act
            var actual = user1.DeleteBudget(budget2.Id);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InvalidDeleteBudget()
        {
            // Arrange
            var user1 = new User() { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget1 = new Budget(user1, "Personal1", 50, Currency.EUR);
            var budget2 = new Budget(user1, "Personal2", 50, Currency.EUR);
            var budget3 = new Budget(user1, "Personal3", 50, Currency.EUR);
            var expected = false;

            // Act
            var actual = user1.DeleteBudget(budget3.Id + 1);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShareCount()
        {
            // Arrange
            var user1 = new User() { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var user2 = new User() { FistName = "Apple", LastName = "Green", Email = "apple.apple@apple.com" };
            var budget1 = new Budget(user1, "Personal1", 50, Currency.EUR);
            user1.Share(user2, budget1.Id);
            var expected = 1;

            // Act
            var actual = user2.SharedBudgets.Count;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
