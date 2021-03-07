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
            var user1 = new User(1) { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget1 = new Budget(1, user1) { Name = "Personal", Currency = Currency.EUR, StartBalance = 50 };
            var budget2 = new Budget(2, user1) { Name = "Personal", Currency = Currency.EUR, StartBalance = 50 };
            var budget3 = new Budget(3, user1) { Name = "Personal", Currency = Currency.EUR, StartBalance = 50 };
            var budget4 = new Budget(4, user1) { Name = "Personal", Currency = Currency.EUR, StartBalance = 50 };
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
            var user1 = new User(1) { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget1 = new Budget(1, user1) { Name = "Personal", Currency = Currency.EUR, StartBalance = 50 };
            var expected = true;

            // Act
            var actual = budget1.Validate();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidateWithoutName()
        {
            // Arrange
            var user1 = new User(1) { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget1 = new Budget(1, user1) {Currency = Currency.UAH, StartBalance = 50 };
            var expected = false;

            // Act
            var actual = budget1.Validate();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidAddTransaction()
        {
            // Arrange
            var user = new User(1) { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var category1 = new Category(1) { Name = "Food", Color = Color.Green };
            var category2 = new Category(2) { Name = "Sport", Color = Color.Yellow };
            user.Categories.Add(category1);
            user.Categories.Add(category2);
            var budget1 = new Budget(1, user) { Name = "Personal", Currency = Currency.UAH, StartBalance = 50};
            budget1.AddCategory(1);
            budget1.AddCategory(2);
            var expected = true;

            // Act
            var actual = budget1.AddTransaction(new Transaction()
            {
                Value = 15.5m,
                Currency = Currency.UAH,
                Date = new System.DateTime(2021, 3, 4, 12, 52, 48),
                Category = category1,
            });

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InValidAddTransaction()
        {
            // Arrange
            var user = new User(1) { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var category1 = new Category(1) { Name = "Food", Color = Color.Green };
            var category2 = new Category(2) { Name = "Sport", Color = Color.Yellow };
            user.Categories.Add(category1);
            user.Categories.Add(category2);
            var budget1 = new Budget(1, user) { Name = "Personal", Currency = Currency.UAH, StartBalance = 50 };
            budget1.AddCategory(1);
            budget1.AddCategory(2);
            var expected = false;

            // Act
            var actual = budget1.AddTransaction(new Transaction()
            {
                Date = new System.DateTime(2021, 3, 4, 12, 52, 48),
                Category = new Category() { Name = "Category", Color = Color.Olive}
            });

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BalanceWithoutTransaction()
        {
            // Arrange
            var user1 = new User(1) { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget1 = new Budget(1, user1) { Name = "Personal", Currency = Currency.UAH, StartBalance = 50 };
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
            var user1 = new User(1) { FistName = "Andrii", LastName = "Chaliuk", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget1 = new Budget(1, user1) { Name = "Personal", Currency = Currency.UAH};
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
            var user = new User(1) { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var category1 = new Category(1) { Name = "Food", Color = Color.Green };
            var category2 = new Category(2) { Name = "Sport", Color = Color.Yellow };
            user.Categories.Add(category1);
            user.Categories.Add(category2);
            var budget1 = new Budget(1, user) { Name = "Personal", Currency = Currency.UAH, StartBalance = 400 };
            budget1.AddCategory(1);
            budget1.AddCategory(2);
            budget1.AddTransaction(new Transaction()
            {
                Value = 150m,
                Currency = Currency.UAH,
                Date = new System.DateTime(2021, 3, 4, 12, 52, 49),
                Category = category1
            });
            budget1.AddTransaction(new Transaction()
            {
                Value = -135m,
                Currency = Currency.UAH,
                Date = new System.DateTime(2021, 3, 4, 12, 52, 50),
                Category = category1
            });
            budget1.AddTransaction(new Transaction()
            {
                Value = -300m,
                Currency = Currency.UAH,
                Date = new System.DateTime(2021, 3, 4, 12, 52, 51),
                Category = category2
            });
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
            var user = new User(1) { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var category1 = new Category(1) { Name = "Food", Color = Color.Green };
            var category2 = new Category(2) { Name = "Sport", Color = Color.Yellow };
            user.Categories.Add(category1);
            user.Categories.Add(category2);
            var budget1 = new Budget(1, user) { Name = "Personal", Currency = Currency.UAH, StartBalance = 400 };
            budget1.AddCategory(1);
            budget1.AddCategory(2);
            budget1.AddTransaction(new Transaction()
            {
                Value = 15m,
                Currency = Currency.EUR,
                Date = new System.DateTime(2021, 3, 4, 12, 52, 49),
                Category = category1
            });
            budget1.AddTransaction(new Transaction()
            {
                Value = -13.5m,
                Currency = Currency.USD,
                Date = new System.DateTime(2021, 3, 4, 12, 52, 50),
                Category = category1
            });
            budget1.AddTransaction(new Transaction()
            {
                Value = -300m,
                Currency = Currency.UAH,
                Date = new System.DateTime(2021, 3, 4, 12, 52, 51),
                Category = category2
            });
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
            var user = new User(1) { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var category1 = new Category(1) { Name = "Food", Color = Color.Green };
            var category2 = new Category(2) { Name = "Sport", Color = Color.Yellow };
            user.Categories.Add(category1);
            user.Categories.Add(category2);
            var budget1 = new Budget(1, user) { Name = "Personal", Currency = Currency.UAH, StartBalance = 50 };
            budget1.AddCategory(1);
            budget1.AddCategory(2);
            budget1.AddTransaction(new Transaction(1)
            {
                Value = 15.5m,
                Currency = Currency.UAH,
                Date = new System.DateTime(2021, 3, 4, 12, 52, 48),
                Category = category1,
            });
            var expected = true;

            // Act
            var actual = budget1.DeleteTransaction(1);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InvalidDeleteTransaction()
        {
            // Arrange
            var user = new User(1) { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var category1 = new Category(1) { Name = "Food", Color = Color.Green };
            var category2 = new Category(2) { Name = "Sport", Color = Color.Yellow };
            user.Categories.Add(category1);
            user.Categories.Add(category2);
            var budget1 = new Budget(1, user) { Name = "Personal", Currency = Currency.UAH, StartBalance = 50 };
            budget1.AddCategory(1);
            budget1.AddCategory(2);
            budget1.AddTransaction(new Transaction(1)
            {
                Value = 15.5m,
                Currency = Currency.UAH,
                Date = new System.DateTime(2021, 3, 4, 12, 52, 48),
                Category = category1,
            });
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
            var user = new User(1) { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var category1 = new Category(1) { Name = "Food", Color = Color.Green };
            var category2 = new Category(2) { Name = "Sport", Color = Color.Yellow };
            user.Categories.Add(category1);
            user.Categories.Add(category2);
            var budget1 = new Budget(1, user) { Name = "Personal", Currency = Currency.UAH, StartBalance = 50 };
            budget1.AddCategory(1);
            budget1.AddCategory(2);
            budget1.AddTransaction(new Transaction()
            {
                Value = -15.5m,
                Currency = Currency.UAH,
                Date = new System.DateTime(2021, 3, 4, 12, 52, 48),
                Category = category1,
            });
            budget1.AddTransaction(new Transaction()
            {
                Value = 40m,
                Currency = Currency.UAH,
                Date = new System.DateTime(2021, 3, 4, 12, 52, 48),
                Category = category1,
            });
            budget1.AddTransaction(new Transaction()
            {
                Value = -30m,
                Currency = Currency.UAH,
                Date = new System.DateTime(2021, 3, 4, 12, 52, 48),
                Category = category1,
            });
            budget1.AddTransaction(new Transaction()
            {
                Value = 100m,
                Currency = Currency.UAH,
                Date = new System.DateTime(2021, 3, 4, 12, 52, 48),
                Category = category1,
            });
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
            var user = new User(1) { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var category1 = new Category(1) { Name = "Food", Color = Color.Green };
            var category2 = new Category(2) { Name = "Sport", Color = Color.Yellow };
            user.Categories.Add(category1);
            user.Categories.Add(category2);
            var budget1 = new Budget(1, user) { Name = "Personal", Currency = Currency.UAH, StartBalance = 50 };
            budget1.AddCategory(1);
            budget1.AddCategory(2);
            budget1.AddTransaction(new Transaction()
            {
                Value = -15.5m,
                Currency = Currency.UAH,
                Date = new System.DateTime(2021, 3, 4, 12, 52, 48),
                Category = category1,
            });
            budget1.AddTransaction(new Transaction()
            {
                Value = 40m,
                Currency = Currency.UAH,
                Date = new System.DateTime(2021, 3, 4, 12, 52, 48),
                Category = category1,
            });
            budget1.AddTransaction(new Transaction()
            {
                Value = -30m,
                Currency = Currency.UAH,
                Date = new System.DateTime(2021, 3, 4, 12, 52, 48),
                Category = category1,
            });
            budget1.AddTransaction(new Transaction()
            {
                Value = 100m,
                Currency = Currency.UAH,
                Date = new System.DateTime(2021, 3, 4, 12, 52, 48),
                Category = category1,
            });
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
            var user = new User(1) { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var category1 = new Category(1) { Name = "Food", Color = Color.Green };
            var category2 = new Category(2) { Name = "Sport", Color = Color.Yellow };
            user.Categories.Add(category1);
            user.Categories.Add(category2);
            var budget1 = new Budget(1, user) { Name = "Personal", Currency = Currency.UAH, StartBalance = 50 };
            budget1.AddCategory(1);
            budget1.AddCategory(2);
            var expected = new List<Transaction>();
            for (var i = 0; i <= 15; ++i)
            {
                var transaction = new Transaction()
                {
                    Value = -15.5m + i,
                    Currency = Currency.UAH,
                    Date = new System.DateTime(2021, 3, 4, 12, 52, 48),
                    Category = category1,
                };
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
                Assert.Equal(expected[i+7], actual2[i]);
            }
        }

        [Fact]
        public void GetCategories()
        {
            // Arrange
            var user = new User(1) { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget = new Budget(1, user) { Name = "Budget1", Currency = Currency.UAH };
            var categories = new List<Category>();
            for (int i = 0; i < 20; ++i)
            {
                var category = new Category(i) { Name = "Category" + i, Color = Color.White };
                user.Categories.Add(category);
                categories.Add(category);
                budget.AddCategory(i);
            }

            // Act
            var actual = budget.GetCategories();

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
            var user = new User(1) { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget = new Budget(1, user) { Name = "Budget1", Currency = Currency.UAH };
            var categories = new List<Category>();
            for (int i = 0; i < 20; ++i)
            {
                var category = new Category(i) { Name = "Category" + i, Color = Color.White };
                user.Categories.Add(category);
                categories.Add(category);
                budget.AddCategory(i);
            }
            var expected = true;

            // Act
            var actual = budget.DeleteCategory(12);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ValidAddCategory()
        {
            // Arrange
            var user = new User(1) { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget = new Budget(1, user) { Name = "Budget1", Currency = Currency.UAH };

            var expected = true;
            user.Categories.Add(new Category(1) { Name = "Category", Color = Color.White });

            // Act
            var actual = budget.AddCategory(1);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InvalidAddCategoryUser()
        {
            // Arrange
            var user = new User(1) { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget = new Budget(1, user) { Name = "Budget1", Currency = Currency.UAH };

            var expected = false;
            user.Categories.Add(new Category(1) { Name = "Category", Color = Color.White });

            // Act
            var actual = budget.AddCategory(2);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InvalidAddCategoryRepeat()
        {
            // Arrange
            var user = new User(1) { LastName = "Chaliuk", FistName = "Andrii", Email = "andrii.chaliuk@ukma.edu.ua" };
            var budget = new Budget(1, user) { Name = "Budget1", Currency = Currency.UAH };

            var expected = false;
            user.Categories.Add(new Category(1) { Name = "Category", Color = Color.White });
            budget.AddCategory(1);

            // Act
            var actual = budget.AddCategory(1);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
