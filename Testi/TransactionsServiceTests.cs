using BusinessLayer;
using BusinessLayer.Budgets;
using BusinessLayer.Transactions;
using BusinessLayer.Users;
using System;
using System.Collections.Generic;
using BudgetsWPF;
using Xunit;
using System.Threading.Tasks;
using System.Threading;

namespace Tests
{
    public class TransactionsServiceTests
    {

        [Fact]
        public async void AddBudgets()
        {
            Thread.Sleep(15000);
            // Arrange
            var user1 = new RegistrationUser() { Login = "andrii3", Password = "passwor", Email = "andrii.chaliuk@ukma.edu.ua", FirstName = "Andrii", LastName = "Chaliuk" };
            var service = new AuthenticationService();
            bool registered = false;
            try
            {
                registered = Task.Run(() => service.RegisterUser(user1)).Result;
            }
            catch (Exception) { }
            var authuser1 = new AuthenticationUser() { Login = "andrii3", Password = "passwor"};
            User user = null;
            try
            {
                user = service.Authenticate(authuser1).Result;
            }
            catch (Exception) { }
            CurrentUser.User = user;
            var budgetsService = new BudgetsService();
            var budget = new Budget(Guid.NewGuid(), "Budget1", 10m, CurrentUser.User, Currency.UAH);
            await budgetsService.AddOrUpdateAsync(budget);
            Thread.Sleep(3000);

            var transactionsService = new TransactionsService(budget.Guid);
            var transaction = new Transaction(Guid.NewGuid(), 10m, Currency.UAH, DateTime.Now);
            await transactionsService.AddOrUpdateAsync(transaction);
            Thread.Sleep(3000);
            var l = Task.Run(transactionsService.GetTransations).Result;

            var expected = 1;
            //Act
            var actual = l.Count;

            // Assert
            Assert.Equal(expected, actual);
            Assert.Equal(transaction.Value, l[0].Value);
            Assert.Equal(transaction.Guid, l[0].Guid);
            Assert.Equal(transaction.Date, l[0].Date);
            Assert.Equal(transaction.Currency, l[0].Currency);

            await transactionsService.DeleteTransaction(Guid.Empty);
            await budgetsService.DeleteBudget(budget.Guid);
        }
    }
}
