using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class User
    {
        public static int InstanceCount { get; private set; }

        private int _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private List<Budget> _budgets;
        private List<SharedBudget> _sharedBudgets;
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

        public List<SharedBudget> SharedBudgets
        {
            get { return _sharedBudgets; }
            private set { _sharedBudgets = value; }
        }

        public User()
        {
            InstanceCount++;
            _budgets = new List<Budget>();
            _categories = new List<Category>();
            _id = InstanceCount;
            _sharedBudgets = new List<SharedBudget>();
        }

        /* public User(int id):this()
        {
            _id = id;
        }*/

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

        /*public Budget CreateBudget(string _name, decimal _startBalance, Currency _currency, string _description = null)
        {
            return new Budget(this, _name, _startBalance, _currency, _description);
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

        /*  public List<Budget> GetBudgets()
        {
            return _budgets.GetRange(0, _budgets.Capacity);
        }*/

        public bool Share(User user, int budgetId)
        {
            if (!user.Validate() || !Validate() || user.Id == Id)
            {
                return false;
            }
            SharedBudget shareBudget1 = null;
            foreach(var budget in _budgets)
            {
                if (budget.Id == budgetId)
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
        }
    }
}