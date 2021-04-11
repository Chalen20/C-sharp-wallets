using Models.Budgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BudgetsWPF
{
    public class BudgetsService
    {
        private static List<Budget> Budgets = new List<Budget>
        {
            new Budget(){Name="wal1", Balance=57.0m },
            new Budget(){Name="wal2", Balance=157.0m },
            new Budget(){Name="wal3", Balance=257.0m },
            new Budget(){Name="wal4", Balance=357.0m },
            new Budget(){Name="wal5", Balance=457.0m },
/*            new Budget(){Name="wal1", Balance=57.0m },
            new Budget(){Name="wal2", Balance=157.0m },
            new Budget(){Name="wal3", Balance=257.0m },
            new Budget(){Name="wal4", Balance=357.0m },
            new Budget(){Name="wal5", Balance=457.0m },
            new Budget(){Name="wal1", Balance=57.0m },
            new Budget(){Name="wal2", Balance=157.0m },
            new Budget(){Name="wal3", Balance=257.0m },
            new Budget(){Name="wal4", Balance=357.0m },
            new Budget(){Name="wal5", Balance=457.0m },
            new Budget(){Name="wal1", Balance=57.0m },
            new Budget(){Name="wal2", Balance=157.0m },
            new Budget(){Name="wal3", Balance=257.0m },
            new Budget(){Name="wal4", Balance=357.0m },
            new Budget(){Name="wal5", Balance=457.0m },
            new Budget(){Name="wal1", Balance=57.0m },
            new Budget(){Name="wal2", Balance=157.0m },
            new Budget(){Name="wal3", Balance=257.0m },
            new Budget(){Name="wal4", Balance=357.0m },
            new Budget(){Name="wal5", Balance=457.0m },
            new Budget(){Name="wal1", Balance=57.0m },
            new Budget(){Name="wal2", Balance=157.0m },
            new Budget(){Name="wal3", Balance=257.0m },
            new Budget(){Name="wal4", Balance=357.0m },
            new Budget(){Name="wal5", Balance=457.0m },*/
        };


        public List<Budget> GetBudgets()
        {
            return Budgets.ToList();
        }
    }
}
