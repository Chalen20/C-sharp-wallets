using BusinessLayer.Transactions;
using DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BudgetsWPF
{
    public class TransactionsService
    {
        private FileDataStorage<Transaction> _storage;

        public TransactionsService(Guid guid)
        {
            _storage = new FileDataStorage<Transaction>(guid.ToString());
        }

        public async Task<List<Transaction>> GetTransations()
        {
            return await Task.Run(async () =>
            {
                Thread.Sleep(2000);
                return (await _storage.GetAllAsync()).OrderByDescending(transaction => transaction.Date).ToList();
            });
        }

        public async Task AddOrUpdateAsync(Transaction transaction)
        {
            await Task.Run(async () =>
            {
                Thread.Sleep(2000);
                await _storage.AddOrUpdateAsync(transaction);
            });
        }

        public async Task DeleteTransaction(Guid guid)
        {
           await Task.Run(() =>
           {
               Thread.Sleep(2000);
               _storage.Delete(guid);
           });
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
