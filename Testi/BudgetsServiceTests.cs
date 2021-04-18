using BusinessLayer;
using BusinessLayer.Budgets;
using BusinessLayer.Users;
using System;
using BudgetsWPF;
using Xunit;
using System.Threading.Tasks;
using System.Threading;

namespace Tests
{
    public class BudgetsServiceTests
    {

        [Fact]
        public async void AddBudgets()
        {
            // Arrange
            var user1 = new RegistrationUser() { Login = "andrii2", Password = "passwor", Email = "andrii.chaliuk@ukma.edu.ua", FirstName = "Andrii", LastName = "Chaliuk" };
            var service = new AuthenticationService();
            bool registered = false;
            try
            {
                registered = Task.Run(() => service.RegisterUser(user1)).Result;
            }
            catch (Exception) { }
            var authuser1 = new AuthenticationUser() { Login = "andrii2", Password = "passwor"};
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
            var l = Task.Run(budgetsService.GetBudgets).Result;
            var expected = 1;
            //Act
            var actual = l.Count;

            // Assert
            Assert.Equal(expected, actual);
            Assert.Equal(budget.Name, l[0].Name);
            Assert.Equal(budget.StartBalance, l[0].StartBalance);
            Assert.Equal(budget.Balance, l[0].Balance);
            Assert.Equal(budget.Currency, l[0].Currency);
            Assert.Equal(budget.Owner.Guid, l[0].Owner.Guid);
            Assert.Equal(budget.Guid, l[0].Guid);

            await budgetsService.DeleteBudget(budget.Guid);
        }
    }
}
