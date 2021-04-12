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

        [Fact]
        public void InstanceCount()
        {
            // Arrange
            var init = Category.InstanceCount;
            var category1 = new Category() { Name = "Category1", Color = Color.Red };
            var category2 = new Category() { Name = "Category2", Color = Color.Blue };
            var category3 = new Category() { Name = "Category3", Color = Color.Green };

            var expected = 3;

            // Act
            var actual = Category.InstanceCount - init;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
