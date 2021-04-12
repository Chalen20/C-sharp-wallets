using BusinessLayer;
using BusinessLayer.Budgets;
using BusinessLayer.Users;
using System;
using Xunit;

namespace Tests
{
    public class UserTests
    {
        [Fact]
        public void ValidValidate()
        {
            // Arrange
            var user = new User(Guid.NewGuid(), "Andrii", "Chaliuk", "andrii.chaliuk@ukma.edu.ua", "Andrii");
            var expected = true;

            // Act
            var actual = user.Validate();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
