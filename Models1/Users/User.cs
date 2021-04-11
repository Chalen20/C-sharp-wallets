using Models.Budgets;
using System;
using System.Collections.Generic;

namespace BudgetsWPF
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
        }

        public Guid Guid{ get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }

        public string Login { get; }

        public List<Budget> Budgets { get; }
    }
}
