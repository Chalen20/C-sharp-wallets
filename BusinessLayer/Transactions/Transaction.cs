using BusinessLayer.Budgets;
using DataStorage;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Transactions
{
    public class Transaction : IStorable
    {
        public Guid Guid { get; }

        public decimal Value { get; set; }

        public Currency Currency { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public Category Category { get; set; }

        public Budget Budget { get; }

        public List<string> Attachments { get; set; }

        public Transaction(Guid guid, /*Budget budget,*/ decimal value, Currency currency, DateTime date, Category category = null, string description = null, List<string> attachments = null)
        {
            Guid = guid;
/*            Budget = budget;*/
            Value = value;
            Currency = currency;
            Date = date;
            Category = category;
            Description = description;
            Attachments = attachments;
            if (Attachments == null)
            {
                Attachments = new List<string>();
            }
        }

        public bool Validate()
        {
            var result = true;
            if (Category == null)
            {
                result = false;
            }
            else if (!Category.Validate())
            {
                result = false;
            }
            return result;
        }
    }
}
