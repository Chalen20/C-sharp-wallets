using BusinessLayer.Budgets;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Users
{
    public class User
    {
        public User(Guid guid, string firstName, string lastName, string email, string login) 
        {
            Guid = guid;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Login = login;
/*            Budgets = new List<Budget>();*/
            Categories = new List<Category>();
/*            SharedBudgets = new List<SharedBudget>();*/
        }

        public Guid Guid{ get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }

        public string Login { get; }

/*        public List<Budget> Budgets { get; }
        public List<SharedBudget> SharedBudgets { get; }*/
        public List<Category> Categories { get; }

        public bool Validate()
        {
            var result = true;
            if (String.IsNullOrWhiteSpace(LastName))
            {
                result = false;
            }
            if (String.IsNullOrWhiteSpace(Email))
            {
                result = false;
            }
            return result;
        }

/*        public bool DeleteBudget(Guid id)
        {
            foreach (var budget in Budgets)
            {
                if (budget.Guid == id)
                {
                    Budgets.Remove(budget);
                    return true;
                }
            }
            return false;
        }

        public bool Share(User user, Guid budgetId)
        {
            if (!user.Validate() || !Validate() || user.Guid == Guid)
            {
                return false;
            }
            SharedBudget shareBudget1 = null;
            foreach (var budget in Budgets)
            {
                if (budget.Guid == budgetId)
                {
                    shareBudget1 = new SharedBudget(budget);
                    break;
                }
            }
            if (shareBudget1 != null)
            {
                user.SharedBudgets.Add(shareBudget1);
                return true;
            }
            return false;
        }*/
    }
}
