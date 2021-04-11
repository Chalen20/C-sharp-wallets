using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Budgets
{
    public class Budget
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Balance})";
        }
    }
}
