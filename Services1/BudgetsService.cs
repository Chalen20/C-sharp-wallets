using DataStorage;
using Models.Budgets;
using System.Collections.Generic;
using System.Linq;

namespace BudgetsWPF
{
    public class BudgetsService
    {
        private static List<Budget> Budgets = new List<Budget>
        {
            new Budget("wal1", 57.0m),
            new Budget("wal2", 157.0m),
            new Budget("wal3", 257.0m),
            new Budget("wal4", 357.0m),
            new Budget("wal5", 457.0m),
        };
/*        private FileDataStorage<Budget> _storage = new FileDataStorage<Budget>();
*/
/*        public async Task<List<Budget>> GetBudgets()
        {
            return await _budgets.GetAllAsync();
        }*/

        public List<Budget> GetBudgets()
        {
            return Budgets.ToList();
        }

/*        public AddOrUpdate()
        {

        }*/
    }
}
