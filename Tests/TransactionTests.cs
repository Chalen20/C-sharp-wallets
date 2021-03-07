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
            var init = Transaction.InstanceCount;
            var transaction1 = new Transaction() 
            {
                Date = new System.DateTime(2021, 3, 6, 12, 17, 15),
                Category = new Category() { Color = Color.Blue, Name = "Category1"}
            };
            var transaction2 = new Transaction()
            {
                Date = new System.DateTime(2021, 3, 6, 12, 17, 16),
                Category = new Category() { Color = Color.White, Name = "Category2" }
            };
            var transaction3 = new Transaction()
            {
                Date = new System.DateTime(2021, 3, 6, 12, 17, 17),
                Category = new Category() { Color = Color.Orange, Name = "Category3" }
            };
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
            var transaction1 = new Transaction()
            {
                Date = new System.DateTime(2021, 3, 6, 12, 17, 15),
                Category = new Category() { Color = Color.Blue, Name = "Category1" }
            };
            var expected = true;

            // Act
            var actual = transaction1.Validate();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateWithoutDate()
        {
            // Arrange
            var transaction1 = new Transaction()
            {
                Category = new Category() { Color = Color.Blue, Name = "Category1" }
            };
            var expected = false;

            // Act
            var actual = transaction1.Validate();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateWithoutCategory()
        {
            // Arrange
            var transaction1 = new Transaction()
            {
                Date = new System.DateTime(2021, 3, 6, 12, 17, 15),
            };
            var expected = false;

            // Act
            var actual = transaction1.Validate();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateWithInvalidCategory()
        {
            // Arrange
            var transaction1 = new Transaction()
            {
                Date = new System.DateTime(2021, 3, 6, 12, 17, 15),
                Category = new Category() {Name = "Category1" }
            };
            var expected = false;

            // Act
            var actual = transaction1.Validate();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
