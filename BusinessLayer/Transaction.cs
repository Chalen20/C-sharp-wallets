using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class Transaction
    {
        private Guid _id;
        private decimal _value;
        private Currency _currency;
        private string _description;
        private DateTime _date;
        private Category _category;
        private Guid _budget;
        private List<string> _attachments;

        public Guid Id
        {
            get { return _id; }
            private set { _id = value; }
        }

        public Guid Budget
        {
            get { return _budget; }
            private set { _budget = value; }
        }

        public decimal Value
        {
            get { return _value; }
            set { _value = value;}
        }

        public Currency Currency
        {
            get { return _currency; }
            set { _currency = value; }
        } 

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public Category Category
        {
            get { return _category; }
            set { _category = value; }
        }

        public List<string> Attachments
        {
            get { return _attachments; }
            set { _attachments = value; }
        }

        public Transaction(Guid budget,decimal value, Currency currency, DateTime date, Category category, string description = null, List<string> attachments = null)
        {
            Budget = budget;
            Value = value;
            Currency = currency;
            Date = date;
            Category = category;
            Description = description;
            Attachments = attachments;
            if (Attachments == null)
            {
                _attachments = new List<string>();
            }
        }

        /*public Transaction(int id): this()
        {
            _id = id;
        }*/

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
