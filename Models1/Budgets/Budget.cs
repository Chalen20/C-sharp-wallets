using BudgetsWPF;
using DataStorage;
using System;

namespace Models.Budgets
{
    public class Budget : IStorable
    {
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public string Description { get; set; }

        public Guid Guid { get; }

        public Budget(string name, decimal balance, string description="")
        {
            Guid = Guid.NewGuid();
            Name = name;
            Balance = balance;
            Description = description;
        }
        /* public override string ToString()
           {
               return $"{Name} ({Balance})";
           }*/
    }
}
