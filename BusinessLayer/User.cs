using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class User
    {
        public static int InstanceCount { get; set; }

        private int _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private List<Budget> _budgets;
        private List<Category> _categories;

        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }

        public string FistName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public List<Category> Categories
        {
            get { return _categories; }
            set { _categories = value; }
        }

        public List<Budget> Budgets
        {
            get { return _budgets; }
            private set { _budgets = value; }
        }

        public User()
        {
            InstanceCount++;
            _budgets = new List<Budget>();
            _categories = new List<Category>();
        }

        public User(int id):this()
        {
            _id = id;
        }

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

        /*public bool CreateBudget(int id)
        {   
            foreach(var budget in _budgets)
            {
                if (budget.Id == id)
                {
                    return false;
                }
            }
            _budgets.Add(new Budget(id, this));
            return true;
        }*/

        public bool DeleteBudget(int id)
        {
            foreach(var budget in _budgets)
            {
                if(budget.Id == id) 
                {
                    if (budget.Owner == this)
                    {
                        _budgets.Remove(budget);
                        return true;
                    }
                }
            }
            return false;
        }

/*        public List<Budget> GetBudgets()
        {
            return _budgets.GetRange(0, _budgets.Capacity);
        }*/

        public bool Share(User user, int budgetId)
        {
            if (!user.Validate() || !Validate() || user.Id == Id)
            {
                return false;
            }
            Budget shareBudget = null;
            foreach(var budget in _budgets)
            {
                if (budget.Id == budgetId)
                {
                    shareBudget = budget;
                    break;
                }
            }
            if (shareBudget != null)
            {
                user._budgets.Add(shareBudget);
                shareBudget.Users.Add(user);
                return true;
            }
            return false;
        }

    }
}