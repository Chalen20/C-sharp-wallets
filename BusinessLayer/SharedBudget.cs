using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer
{
    public class SharedBudget
    {
/*        public static int InstanceCount { get; private set; }*/
        private Budget _budget;

        public int Id
        {
            get { return _budget.Id; }
        }

        public string Name
        {
            get { return _budget.Name; }
        }

        public decimal StartBalance
        {
            get { return _budget.StartBalance; }
        }

        public decimal Balance
        {
            get { return _budget.Balance; }
        }

        public string Description
        {
            get { return _budget.Description; }
        }

        public Currency Currency
        {
            get { return _budget.Currency; }
        }

        public User Owner
        {
            get { return _budget.Owner; }
        }

        public SharedBudget(Budget budget)
        {
            _budget = budget;
        }

        public bool Validate()
        {
            return _budget.Validate();
        }

        public bool AddTransaction(decimal value, Currency currency, DateTime date, Category category, string description = null, List<string> attachments = null)
        {
            return _budget.AddTransaction(value, currency, date, category, description, attachments);
        }

        public decimal GetIncome()
        {
            return _budget.GetIncome();
        }

        public decimal GetExpenses()
        {
            return _budget.GetExpenses();
        }

        public List<Transaction> GetTransactions(int from, int to)
        {
            return _budget.GetTransactions(from, to);
        }

        public List<Category> GetCategories()
        {
            return _budget.GetCategories();
        }
    }
}
