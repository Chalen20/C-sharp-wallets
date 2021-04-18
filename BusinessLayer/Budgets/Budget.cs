using BusinessLayer.Users;
using BusinessLayer.Transactions;
using DataStorage;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Budgets
{
    public class Budget : IStorable
    {
        public string Name { get; set; }

        public decimal StartBalance { get; set; }

        public Currency Currency { get; set; }

        public decimal Balance { get; set; }

        public string Description { get; set; }

        public Guid Guid { get; }

        public User Owner { get; }

        public List<Transaction> Transactions { get; }

        public List<Category> Categories { get; }

        public Budget(Guid guid, string name, decimal balance, User owner, Currency currency, string description="")
        {
            Guid = guid;
            Name = name;
            Balance = balance;
            Description = description;
            Owner = owner;
            Currency = currency;
            Transactions = new List<Transaction>();
            Categories = new List<Category>();
/*            owner.Budgets.Add(this);*/
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

        /*      private decimal Convert(Transaction transaction)
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
              }*/

/*        public bool AddTransaction(decimal value, Currency currency, DateTime date, Category category, string description = null, List<string> attachments = null)
        {
            foreach (var c in Categories)
            {
                if (c == category)
                {
                    var transaction = new Transaction(Guid.NewGuid(), value, currency, date, category, description, attachments);
                    Transactions.Add(transaction);
                    Balance += Convert(transaction);
                    return true;
                }
            }
            return false;
        }

        public bool AddTransaction(Transaction transaction)
        {
            foreach (var c in Categories)
            {
                if (c == transaction.Category)
                {
                    Transactions.Add(transaction);
                    Balance += Convert(transaction);
                    return true;
                }
            }
            return false;
        }


        public bool DeleteTransaction(Guid id)
        {
            foreach (var transaction in Transactions)
            {
                if (transaction.Guid == id)
                {
                    Transactions.Remove(transaction);
                    return true;
                }
            }
            return false;
        }

        private decimal GetCostsOrReceipts(bool sign)
        {
            var result = 0m;
            foreach (var transaction in Transactions)
            {
                if (DateTime.Today.Month == transaction.Date.Month)
                {
                    if (sign && transaction.Value > 0)
                    {
                        result += transaction.Value;
                    }
                    else if (!sign && transaction.Value < 0)
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

        public double GetBalance()
        {
            var result = StartBalance;
            foreach (var transaction in Transactions)
            {
                result += transaction.Value;
            }
            return (double)result;
        }

        public List<Transaction> GetTransactions(int from, int to)
        {
            if (from > to || to - from > 10 || from > Transactions.Count)
            {
                return new List<Transaction>();
            }
            if (to > Transactions.Count)
            {
                return Transactions.GetRange(from, Transactions.Count - from);
            }
            else
            {
                return Transactions.GetRange(from, to - from);
            }
        }*/

        public List<Category> GetCategories()
        {
            var categories = new List<Category>();
            foreach (var category in Categories)
            {
                categories.Add(category);
            }
            return categories;
        }

        public bool AddCategory(Guid category_id)
        {
            foreach (var category in Categories)
            {
                if (category.Guid == category_id)
                {
                    return false;
                }
            }
            foreach (var category in Owner.Categories)
            {
                if (category.Guid == category_id)
                {
                    if (category.Validate())
                    {
                        Categories.Add(category);
                        return true;
                    }
                }
            }
            return false;
        }

        public bool DeleteCategory(Guid id)
        {
            foreach (var category in Categories)
            {
                if (category.Guid == id)
                {
                    Categories.Remove(category);
                    return true;
                }
            }
            return false;
        }
        /* public override string ToString()
           {
               return $"{Name} ({Balance})";
           }*/
    }
}
