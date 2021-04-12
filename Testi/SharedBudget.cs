using BusinessLayer;
using System.Drawing;
using Xunit;

namespace Tests
{
    public class SharedBudget
    {
        [Fact]
        public void ValidValidate()
        {
            // Arrange
            var user1 = new User() { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var user2 = new User() { FistName = "Andrii2", LastName = "Chaliuk2", Email = "andrii.chaliuk@ukma.edu.ua2" };
            var budget1 = new Budget(user1, "Personal4", 50, Currency.EUR);
            user1.Share(user2, budget1.Id);
            var expected = true;

            // Act
            var actual = user2.SharedBudgets[0].Validate();

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void AddTransaction()
        {
            // Arrange
            var user1 = new User() { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var user2 = new User() { FistName = "Andrii2", LastName = "Chaliuk2", Email = "andrii.chaliuk@ukma.edu.ua2" };
            var budget1 = new Budget(user1, "Personal4", 50, Currency.EUR);
            var category = new Category() { Name = "Category", Color = Color.Blue };
            user1.Categories.Add(category);
            user2.Categories.Add(category);
            budget1.AddCategory(category.Id);
            user1.Share(user2, budget1.Id);
            var expected = true;

            // Act
            var actual = user2.SharedBudgets[0].AddTransaction(
                50, Currency.UAH, 
                new System.DateTime(2021, 3, 8, 2, 20, 20), 
                category
            );

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetIncome()
        {
            // Arrange
            var user1 = new User() { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var user2 = new User() { FistName = "Andrii2", LastName = "Chaliuk2", Email = "andrii.chaliuk@ukma.edu.ua2" };
            var budget1 = new Budget(user1, "Personal4", 50, Currency.EUR);
            var category = new Category() { Name = "Category", Color = Color.Blue };
            user1.Categories.Add(category);
            user2.Categories.Add(category);
            budget1.AddCategory(category.Id);
            user1.Share(user2, budget1.Id);
            var expected = 514;
            user2.SharedBudgets[0].AddTransaction(
                -140, Currency.UAH,
                new System.DateTime(2021, 3, 8, 2, 20, 20),
                category
            );
            user2.SharedBudgets[0].AddTransaction(
                -207, Currency.UAH,
                new System.DateTime(2021, 3, 8, 2, 20, 20),
                category
            );
            user2.SharedBudgets[0].AddTransaction(
                514, Currency.UAH,
                new System.DateTime(2021, 3, 8, 2, 20, 20),
                category
            );

            // Act
            var actual = user2.SharedBudgets[0].GetIncome();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetExpenses()
        {
            // Arrange
            var user1 = new User() { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var user2 = new User() { FistName = "Andrii2", LastName = "Chaliuk2", Email = "andrii.chaliuk@ukma.edu.ua2" };
            var budget1 = new Budget(user1, "Personal4", 50, Currency.EUR);
            var category = new Category() { Name = "Category", Color = Color.Blue };
            user1.Categories.Add(category);
            user2.Categories.Add(category);
            budget1.AddCategory(category.Id);
            user1.Share(user2, budget1.Id);
            var expected = 347;
            user2.SharedBudgets[0].AddTransaction(
                -140, Currency.UAH,
                new System.DateTime(2021, 3, 8, 2, 20, 20),
                category
            );
            user2.SharedBudgets[0].AddTransaction(
                -207, Currency.UAH,
                new System.DateTime(2021, 3, 8, 2, 20, 20),
                category
            );
            user2.SharedBudgets[0].AddTransaction(
                514, Currency.UAH,
                new System.DateTime(2021, 3, 8, 2, 20, 20),
                category
            );

            // Act
            var actual = user2.SharedBudgets[0].GetExpenses();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTransactions()
        {
            // Arrange
            var user1 = new User() { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var user2 = new User() { FistName = "Andrii2", LastName = "Chaliuk2", Email = "andrii.chaliuk@ukma.edu.ua2" };
            var budget1 = new Budget(user1, "Personal4", 50, Currency.EUR);
            var category = new Category() { Name = "Category", Color = Color.Blue };
            user1.Categories.Add(category);
            user2.Categories.Add(category);
            budget1.AddCategory(category.Id);
            user1.Share(user2, budget1.Id);
            user2.SharedBudgets[0].AddTransaction(
                -140, Currency.UAH,
                new System.DateTime(2021, 3, 8, 2, 20, 20),
                category
            );
            user2.SharedBudgets[0].AddTransaction(
                -207, Currency.UAH,
                new System.DateTime(2021, 3, 8, 2, 20, 20),
                category
            );
            user2.SharedBudgets[0].AddTransaction(
                514, Currency.UAH,
                new System.DateTime(2021, 3, 8, 2, 20, 20),
                category
            );
            var expected = 3;

            // Act
            var actual = user2.SharedBudgets[0].GetTransactions(0, 4).Count;

            // Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void GetCategories()
        {
            // Arrange
            var user1 = new User() { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var user2 = new User() { FistName = "Andrii2", LastName = "Chaliuk2", Email = "andrii.chaliuk@ukma.edu.ua2" };
            var budget1 = new Budget(user1, "Personal4", 50, Currency.EUR);
            var category1 = new Category() { Name = "Category1", Color = Color.Blue };
            var category2 = new Category() { Name = "Category2", Color = Color.White };
            var category3 = new Category() { Name = "Category3", Color = Color.Black };
            user1.Categories.Add(category1);
            user1.Categories.Add(category2);
            user1.Categories.Add(category3);
            user2.Categories.Add(category1);
            budget1.AddCategory(category1.Id);
            budget1.AddCategory(category2.Id);
            budget1.AddCategory(category3.Id);
            user1.Share(user2, budget1.Id);
            user2.SharedBudgets[0].AddTransaction(
                -140, Currency.UAH,
                new System.DateTime(2021, 3, 8, 2, 20, 20),
                category1
            );
            user2.SharedBudgets[0].AddTransaction(
                -207, Currency.UAH,
                new System.DateTime(2021, 3, 8, 2, 20, 20),
                category2
            );
            user2.SharedBudgets[0].AddTransaction(
                514, Currency.UAH,
                new System.DateTime(2021, 3, 8, 2, 20, 20),
                category3
            );
            var expected = 3;

            // Act
            var actual = user2.SharedBudgets[0].GetCategories().Count;

            // Assert
            Assert.Equal(expected, actual);
        }

    }
}
