using BusinessLayer;
using BusinessLayer.Budgets;
using BusinessLayer.Users;
using System;
using System.Drawing;
using Xunit;

namespace Tests
{
    public class TransactionTests
    {
        [Fact]
        public void ValidValidate()
        {
            // Arrange
            var user = new User(Guid.NewGuid(), "Andrii", "Chaliuk", "andrii.chaliuk@ukma.edu.ua", "Andrii");
            var budget = new Budget(Guid.NewGuid(), "Budget", 50, user, Currency.UAH);
            var transaction1 = new Transaction(
                budget.Guid, 50, Currency.UAH,
                new System.DateTime(2021, 3, 6, 12, 17, 15),
                new Category() { Color = Color.Blue, Name = "Category1" }
            );
            var expected = true;

            // Act
            var actual = transaction1.Validate();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
