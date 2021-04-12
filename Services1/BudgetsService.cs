using BusinessLayer.Budgets;
using BusinessLayer.Users;
using DataStorage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BudgetsWPF
{
    public class BudgetsService
    {
/*        private static List<Budget> Budgets = new List<Budget>
        {
            new Budget("wal1", 57.0m),
            new Budget("wal2", 157.0m),
            new Budget("wal3", 257.0m),
            new Budget("wal4", 357.0m),
            new Budget("wal5", 457.0m),
        };*/
        private FileDataStorage<Budget> _storage = new FileDataStorage<Budget>(CurrentUser.User.Guid.ToString("N"));

        public async Task<List<Budget>> GetBudgets()
        {
            return await _storage.GetAllAsync();
        }

        public async Task AddOrUpdateAsync(Budget budget)
        {
            await _storage.AddOrUpdateAsync(budget);
        }

        public void DeleteBudget(Guid guid)
        {
            _storage.Delete(guid);
        }


/*        public List<Budget> GetBudgets()
        {
            return Budgets.ToList();
        }*/

/*        public AddOrUpdate()
        {

        }*/
    }
}
