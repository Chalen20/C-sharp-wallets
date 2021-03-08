using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class Budget
    {
        public static int InstanceCount { get; set; }

        private int _id;
        private string _name;
        private decimal _startBalance;
        private decimal _balance;
        private string _description;
        private Currency _currency;
        //private int _userId;
        private User _owner;
        private List<Transaction> _transactions;
        protected List<Category> _categories;

        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public decimal StartBalance
        {
            get { return _startBalance; }
            set {
                Balance -= _startBalance;
                _startBalance = value;
                Balance += _startBalance;
            }
        }

        public decimal Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public Currency Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

       /* public int UserId
        {
            get { return _userId; }
            private set { _userId = value; }
        }*/

        public User Owner
        {
            get { return _owner; }
            private set { _owner = value; }
        }

        /*public List<Transaction> Transactions
        {
            get { return _transactions; }
            set { _transactions = value; }
        }*/

        /* public List<Category> Categories
         {
             get { return _categories; }
             set { _categories = value; }
         }*/

        /*public Budget()
        {
            InstanceCount++;
            Balance = StartBalance;
            _transactions = new List<Transaction>();
            _categories = new List<Category>();
            _users = new List<User>();
        }

        public Budget(int id) : this()
        {
            _id = id;
        }

        public Budget(int id, User user) : this(id)
        {
            _owner = user;
            user.Budgets.Add(this);
        }*/

        /*public Budget(User user)
        {
            InstanceCount++;
            Balance = StartBalance;
            _transactions = new List<Transaction>();
            _categories = new List<Category>();
            _users = new List<User>();
            _id = InstanceCount;
            _owner = user;
            user.Budgets.Add(this);
        }*/

        public Budget(User user, string name, decimal startBalance, Currency currency)
        {
            InstanceCount++;
            StartBalance = startBalance;
            Balance = StartBalance;
            Name = name;
            Currency = currency;
            _transactions = new List<Transaction>();
            _categories = new List<Category>();
            _id = InstanceCount;
            _owner = user;
            user.Budgets.Add(this);
        }

        public Budget(User user, string name, decimal startBalance, Currency currency, string description): this(user, name, startBalance, currency)
        {
            Description = description;
        }

        public bool Validate()
        {
            var result = true;
            if (String.IsNullOrWhiteSpace(Name))
            {
                result = false;
            }
            return result;
        }

        private decimal Convert(Transaction transaction)
        {
            if (transaction.Currency == Currency)
            {
                return transaction.Value;
            }
            if (transaction.Currency == Currency.UAH)
            {
                if (Currency == Currency.USD)
                {
                    return transaction.Value * 0.036m;
                }
                if (Currency == Currency.EUR)
                {
                    return transaction.Value * 0.030m;
                }
            }
            if (transaction.Currency == Currency.USD)
            {
                if (Currency == Currency.UAH)
                {
                    return transaction.Value * 27.75m;
                }
                if (Currency == Currency.EUR)
                {
                    return transaction.Value * 0.84m;
                }
            }
            if (transaction.Currency == Currency.EUR)
            {
                if (Currency == Currency.UAH)
                {
                    return transaction.Value * 33.07m;
                }
                if (Currency == Currency.USD)
                {
                    return transaction.Value * 1.19m;
                }
            }
            return 0;
        }

        public bool AddTransaction(decimal value, Currency currency, DateTime date, Category category, string description = null, List<string> attachments = null)
        {
            foreach (var c in _categories)
            {
                if (c == category)
                {
                    var transaction = new Transaction(this, value, currency, date, category, description, attachments);
                    _transactions.Add(transaction);
                    Balance += Convert(transaction);
                    return true;
                }
            }
            return false;
        }

        public bool AddTransaction(Transaction transaction)
        {
            foreach (var c in _categories)
            {
                if (c == transaction.Category)
                {
                    _transactions.Add(transaction);
                    Balance += Convert(transaction);
                    return true;
                }
            }
            return false;
        }

        public bool DeleteTransaction(int id)
        {
            foreach (var transaction in _transactions)
            {
                if (transaction.Id == id)
                {
                    _transactions.Remove(transaction);
                    return true;
                }
            }
            return false;
        }

        private decimal GetCostsOrReceipts(bool sign)
        {
            var result = 0m;
            foreach (var transaction in _transactions)
            {
                if (DateTime.Today.Month == transaction.Date.Month)
                {
                    if (sign && transaction.Value > 0)
                    {
                        result += transaction.Value;
                    }
                    else if(!sign && transaction.Value < 0)
                    {
                        result += transaction.Value;
                    }
                }
            }
            return Math.Abs(result);
        }

        public decimal GetIncome()
        {
            return GetCostsOrReceipts(true);
        }

        public decimal GetExpenses()
        {
            return GetCostsOrReceipts(false);
        }

        /*public double Balance()
        {
            var result = StartBalance; 
            foreach (var transaction in _transactions)
            {
                result += transaction.Value;
            }
            return (double) result;
        }*/

        public List<Transaction> GetTransactions(int from, int to)
        {
            if (from > to || to - from > 10 || from > _transactions.Count)
            {
                return new List<Transaction>();
            }
            if (to > _transactions.Count)
            {
                return _transactions.GetRange(from, _transactions.Count - from);
            }
            else
            {
                return _transactions.GetRange(from, to - from);
            }
        }

        public List<Category> GetCategories()
        {
            var categories = new List<Category>();
            foreach(var category in _categories)
            {
                categories.Add(category);
            }
            return categories;
        }

        public bool AddCategory(int category_id)
        {
            foreach(var category in _categories)
            {
                if(category.Id == category_id) {
                    return false;
                }
            }
            foreach(var category in Owner.Categories)
            {
                if(category.Id == category_id)
                {
                    if (category.Validate())
                    {
                        _categories.Add(category);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool DeleteCategory(int id)
        {
            foreach (var category in _categories)
            {
                if (category.Id == id)
                {
                    _categories.Remove(category);
                    return true;
                }
            }
            return false;
        }
    }
}
