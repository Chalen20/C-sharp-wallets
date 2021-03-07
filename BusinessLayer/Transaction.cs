using System;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class Transaction
    {
        public static int InstanceCount { get; set; }

        private int _id;
        private decimal _value;
        private Currency _currency;
        private string _description;
        private DateTime? _date;
        private Category _category;
        private List<string> _attachments;

        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }

        public decimal Value
        {
            get { return _value; }
            set { _value = value; }
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

        public DateTime? Date
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

        public Transaction()
        {
            InstanceCount++;
            _attachments = new List<string>();
        }

        public Transaction(int id): this()
        {
            _id = id;
        }

        public bool Validate()
        {
            var result = true;
            if (Date == null)
            {
                result = false;
            }
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
