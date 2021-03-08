using BusinessLayer;
using System.Collections.Generic;
using System.Drawing;
using Xunit;

namespace Tests
{
    public class BudgetTests
    {
        [Fact]
        public void InstanceCount()
        {
            // Arrange
            var init = Budget.InstanceCount;
            var user1 = new User() { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            new Budget(user1, "Personal1", 50, Currency.EUR);
            new Budget(user1, "Personal2", 50, Currency.EUR);
            new Budget(user1, "Personal3", 50, Currency.EUR);
            new Budget(user1, "Personal4", 50, Currency.EUR);
            var expected = 4;

            // Act
            var actual = Budget.InstanceCount - init;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidValidate()
        {
            // Arrange
            var user1 = new User() { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget1 = new Budget(user1, "Personal4", 50, Currency.EUR);
            var expected = true;

            // Act
            var actual = budget1.Validate();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidAddTransaction()
        {
            // Arrange
            var user = new User() { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var category1 = new Category() { Name = "Food", Color = Color.Green };
            var category2 = new Category() { Name = "Sport", Color = Color.Yellow };
            user.Categories.Add(category1);
            user.Categories.Add(category2);
            var budget1 = new Budget(user, "Personal4", 50, Currency.EUR);
            budget1.AddCategory(category1.Id);
            budget1.AddCategory(category2.Id);
            var expected = true;

            // Act
            var actual = budget1.AddTransaction(
                15.5m, Currency.UAH,
                new System.DateTime(2021, 3, 4, 12, 52, 48),
                category1
            );

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InValidAddTransaction()
        {
            // Arrange
            var user = new User() { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var category1 = new Category() { Name = "Food", Color = Color.Green };
            var category2 = new Category() { Name = "Sport", Color = Color.Yellow };
            user.Categories.Add(category1);
            user.Categories.Add(category2);
            var budget1 = new Budget(user, "Personal4", 50, Currency.EUR);
            budget1.AddCategory(1);
            budget1.AddCategory(2);
            var expected = false;

            // Act
            var actual = budget1.AddTransaction(
                15.5m, Currency.UAH,
                new System.DateTime(2021, 3, 4, 12, 52, 48),
                new Category() { Name = "Category", Color = Color.Olive }
            );

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BalanceWithoutTransaction()
        {
            // Arrange
            var user1 = new User() { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget1 = new Budget(user1, "Personal4", 50, Currency.EUR);
            var expected = 50;

            // Act
            var actual = budget1.Balance;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BalanceWithoutTransactionAndStartBalance()
        {
            // Arrange
            var user1 = new User() { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget1 = new Budget(user1, "Personal4", 0, Currency.EUR);
            var expected = 0;

            // Act
            var actual = budget1.Balance;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BalanceWithTransaction()
        {
            // Arrange
            var user = new User() { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var category1 = new Category() { Name = "Food", Color = Color.Green };
            var category2 = new Category() { Name = "Sport", Color = Color.Yellow };
            user.Categories.Add(category1);
            var budget1 = new Budget(user, "Personal4", 400, Currency.UAH);
            budget1.AddCategory(category1.Id);
            user.Categories.Add(category2);
            budget1.AddCategory(category2.Id);
            budget1.AddTransaction(
                150m, Currency.UAH,
                new System.DateTime(2021, 3, 4, 12, 52, 49),
                category1
            );
            budget1.AddTransaction(
                -135m, Currency.UAH,
                new System.DateTime(2021, 3, 4, 12, 52, 50),
                category1
            );
            budget1.AddTransaction(
                -300m, Currency.UAH,
                new System.DateTime(2021, 3, 4, 12, 52, 51),
                category2
            );
            var expected = 115;

            // Act
            var actual = budget1.Balance;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BalanceWithTransactionCurrency()
        {
            // Arrange
            var user = new User() { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var category1 = new Category() { Name = "Food", Color = Color.Green };
            var category2 = new Category() { Name = "Sport", Color = Color.Yellow };
            user.Categories.Add(category1);
            user.Categories.Add(category2);
            var budget1 = new Budget(user, "Personal4", 400, Currency.UAH);
            budget1.AddCategory(category1.Id);
            budget1.AddCategory(category2.Id);
            budget1.AddTransaction(
                15m, Currency.EUR,
                new System.DateTime(2021, 3, 4, 12, 52, 49),
                category1
            );
            budget1.AddTransaction(
                -13.5m, Currency.USD,
                new System.DateTime(2021, 3, 4, 12, 52, 50),
                category1
            );
            budget1.AddTransaction(
                -300m,Currency.UAH,
                new System.DateTime(2021, 3, 4, 12, 52, 51),
                category2
            );
            var expected = 221.425m;

            // Act
            var actual = budget1.Balance;

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidDeleteTransaction()
        {
            // Arrange
            var user = new User() { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var category1 = new Category() { Name = "Food", Color = Color.Green };
            var category2 = new Category() { Name = "Sport", Color = Color.Yellow };
            user.Categories.Add(category1);
            user.Categories.Add(category2);
            var budget1 = new Budget(user, "Personal4", 50, Currency.EUR);
            budget1.AddCategory(category1.Id);
            budget1.AddCategory(category2.Id);
            var transaction = new Transaction(
                budget1,
                15.5m,
                Currency.UAH,
                new System.DateTime(2021, 3, 4, 12, 52, 48),
                category1
            );
            budget1.AddTransaction(transaction);
            var expected = true;

            // Act
            var actual = budget1.DeleteTransaction(transaction.Id);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InvalidDeleteTransaction()
        {
            // Arrange
            var user = new User() { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var category1 = new Category() { Name = "Food", Color = Color.Green };
            var category2 = new Category() { Name = "Sport", Color = Color.Yellow };
            user.Categories.Add(category1);
            user.Categories.Add(category2);
            var budget1 = new Budget(user, "Personal4", 50, Currency.EUR);
            budget1.AddCategory(1);
            budget1.AddCategory(2);
            budget1.AddTransaction(
                15.5m, Currency.UAH,
                new System.DateTime(2021, 3, 4, 12, 52, 48),
                category1
            );
            var expected = false;

            // Act
            var actual = budget1.DeleteTransaction(2);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetIncome()
        {
            // Arrange
            var user = new User() { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var category1 = new Category() { Name = "Food", Color = Color.Green };
            var category2 = new Category() { Name = "Sport", Color = Color.Yellow };
            user.Categories.Add(category1);
            user.Categories.Add(category2);
            var budget1 = new Budget(user, "Personal4", 50, Currency.EUR);
            budget1.AddCategory(category1.Id);
            budget1.AddCategory(category2.Id);
            budget1.AddTransaction(
                -15.5m, Currency.UAH,
                new System.DateTime(2021, 3, 4, 12, 52, 48),
                category1
            );
            budget1.AddTransaction(
                40m, Currency.UAH,
                new System.DateTime(2021, 3, 4, 12, 52, 48),
                category1
            );
            budget1.AddTransaction(
                -30m, Currency.UAH,
                new System.DateTime(2021, 3, 4, 12, 52, 48),
                category1
            );
            budget1.AddTransaction(
                100m, Currency.UAH,
                new System.DateTime(2021, 3, 4, 12, 52, 48),
                category1
            );
            var expected = 140;

            // Act
            var actual = budget1.GetIncome();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetExpenses()
        {
            // Arrange
            var user = new User() { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var category1 = new Category() { Name = "Food", Color = Color.Green };
            var category2 = new Category() { Name = "Sport", Color = Color.Yellow };
            user.Categories.Add(category1);
            user.Categories.Add(category2);
            var budget1 = new Budget(user, "Personal4", 50, Currency.EUR);
            budget1.AddCategory(category1.Id);
            budget1.AddCategory(category2.Id);
            budget1.AddTransaction(
                -15.5m,
                Currency.UAH,
                new System.DateTime(2021, 3, 4, 12, 52, 48),
                category1
            );
            budget1.AddTransaction(
                40m,
                Currency.UAH,
                new System.DateTime(2021, 3, 4, 12, 52, 48),
                category1
            );
            budget1.AddTransaction(
                -30m,Currency.UAH,
                new System.DateTime(2021, 3, 4, 12, 52, 48),
                category1
            );
            budget1.AddTransaction(
                100m,
                Currency.UAH,
                new System.DateTime(2021, 3, 4, 12, 52, 48),
                category1
            );
            var expected = 45.5m;

            // Act
            var actual = budget1.GetExpenses();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTransactions()
        {
            // Arrange
            var user = new User() { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var category1 = new Category() { Name = "Food", Color = Color.Green };
            var category2 = new Category() { Name = "Sport", Color = Color.Yellow };
            user.Categories.Add(category1);
            user.Categories.Add(category2);
            var budget1 = new Budget(user, "Personal4", 50, Currency.EUR);
            budget1.AddCategory(category1.Id);
            budget1.AddCategory(category2.Id);
            var expected = new List<Transaction>();
            for (var i = 0; i <= 15; ++i)
            {
                var transaction = new Transaction(
                    budget1, -15.5m + i, Currency.UAH,
                    new System.DateTime(2021, 3, 4, 12, 52, 48),
                    category1
                );
                budget1.AddTransaction(transaction);
                expected.Add(transaction);
            }

            // Act
            var actual = budget1.GetTransactions(0, 10);
            var actual2 = budget1.GetTransactions(7, 13);

            // Assert
            for (var i = 0; i < 10; ++i)
            {
                Assert.Equal(expected[i], actual[i]);
            }
            for (var i = 0; i < 5; ++i)
            {
                Assert.Equal(expected[i + 7], actual2[i]);
            }
        }

        [Fact]
        public void GetCategories()
        {
            // Arrange
            var user = new User() { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget1 = new Budget(user, "Personal4", 50, Currency.EUR);
            var categories = new List<Category>();
            for (int i = 1; i <= 20; ++i)
            {
                var category = new Category() { Name = "Category" + i, Color = Color.White };
                user.Categories.Add(category);
                categories.Add(category);
                budget1.AddCategory(category.Id);
            }

            // Act
            var actual = budget1.GetCategories();

            // Assert
            for (int i = 0; i < 20; ++i)
            {
                Assert.Equal(categories[i], actual[i]);
            }
        }

        [Fact]
        public void DeleteCategories()
        {
            // Arrange
            var user = new User() { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget1 = new Budget(user, "Personal4", 50, Currency.EUR);
            var categories = new List<Category>();
            for (int i = 0; i < 20; ++i)
            {
                var category = new Category() { Name = "Category" + i, Color = Color.White };
                user.Categories.Add(category);
                categories.Add(category);
                budget1.AddCategory(i);
            }
            var expected = true;

            // Act
            var actual = budget1.DeleteCategory(12);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidAddCategory()
        {
            // Arrange
            var user = new User() { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget1 = new Budget(user, "Personal4", 50, Currency.EUR);

            var expected = true;
            var category = new Category() { Name = "Category", Color = Color.White };
            user.Categories.Add(category);

            // Act
            var actual = budget1.AddCategory(category.Id);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InvalidAddCategoryUser()
        {
            // Arrange
            var user = new User() { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget1 = new Budget(user, "Personal4", 50, Currency.EUR);

            var expected = false;
            user.Categories.Add(new Category() { Name = "Category", Color = Color.White });

            // Act
            var actual = budget1.AddCategory(2);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InvalidAddCategoryRepeat()
        {
            // Arrange
            var user = new User() { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget1 = new Budget(user, "Personal4", 50, Currency.EUR);

            var expected = false;
            user.Categories.Add(new Category() { Name = "Category", Color = Color.White });
            budget1.AddCategory(1);

            // Act
            var actual = budget1.AddCategory(1);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
