using BusinessLayer;
using System.Drawing;
using Xunit;

namespace Tests
{
    public class CategoryTests
    {
        [Fact]
        public void ValidValidate()
        {
            // Arrange
            var category = new Category() { Name = "Category1", Color = Color.Red };
            var expected = true;

            // Act
            var actual = category.Validate();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateWithoutColor()
        {
            // Arrange
            var category = new Category() { Name = "Category1" };
            var expected = false;

            // Act
            var actual = category.Validate();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateWithoutName()
        {
            // Arrange
            var category = new Category() { Color = Color.Red };
            var expected = false;

            // Act
            var actual = category.Validate();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
