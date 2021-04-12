using BudgetsWPF;
using DataStorage;
using System;

namespace Models.Budgets
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

        public Budget(string name, decimal balance, User owner, string description="")
        {
            Guid = Guid.NewGuid();
            Name = name;
            Balance = balance;
            Description = description;
            Owner = owner;
        }
        /* public override string ToString()
           {
               return $"{Name} ({Balance})";
           }*/
    }
}
