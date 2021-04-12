using BusinessLayer;
using System.Drawing;
using Xunit;

namespace Tests
{
    public class TransactionTests
    {
        [Fact]
        public void InstanceCount()
        {
            // Arrange
            var user = new User();
            var budget = new Budget(user, "Budget", 50, Currency.UAH);
            var init = Transaction.InstanceCount;
            var transaction1 = new Transaction(
                budget, 50, Currency.UAH,
                new System.DateTime(2021, 3, 6, 12, 17, 15),
                new Category() { Color = Color.Blue, Name = "Category1" }
            );
            var transaction2 = new Transaction(
                budget, 50, Currency.UAH,
                new System.DateTime(2021, 3, 6, 12, 17, 16),
                new Category() { Color = Color.White, Name = "Category2" }
            );
            var transaction3 = new Transaction(
                budget, 50, Currency.UAH,
                new System.DateTime(2021, 3, 6, 12, 17, 17),
                new Category() { Color = Color.Orange, Name = "Category3" }
            );
            var expected = 3;

            // Act
            var actual = Transaction.InstanceCount - init;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidValidate()
        {
            // Arrange
            var user = new User();
            var budget = new Budget(user, "Budget", 50, Currency.UAH);
            var transaction1 = new Transaction(
                budget, 50, Currency.UAH,
                new System.DateTime(2021, 3, 6, 12, 17, 15),
                new Category() { Color = Color.Blue, Name = "Category1" }
            );
            var expected = true;

            // Act
            var actual = transaction1.Validate();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateWithInvalidCategory()
        {
            // Arrange
            var user = new User();
            var budget = new Budget(user, "Budget", 50, Currency.UAH);
            var transaction1 = new Transaction(
                budget, 50, Currency.UAH,
                new System.DateTime(2021, 3, 6, 12, 17, 15),
                new Category() { Name = "Category1" }
            );
            var expected = false;

            // Act
            var actual = transaction1.Validate();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
