using BudgetsWPF.Budgets;
using BudgetsWPF.Navigation;
using BusinessLayer;
using BusinessLayer.Transactions;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BudgetsWPF.Transactions
{
    public class TransactionsViewModel : BindableBase, IDataErrorInfo/*, INavigatable<MainNavigatableTypes>*/
    {
        private TransactionsService _service;
        private int _startBound;
        private int _endBound;
        private TransactionsDetailsViewModel _currentTransaction;
        private bool _isEnable = true;
        private bool _canApplyRange = false;
        public BudgetsDetailsViewModel CurrentBudget { get; set; }
        public DelegateCommand AddTransactionCommand { get; }
        public DelegateCommand DeleteAllTracsactionsCommand { get; }
        public DelegateCommand DeleteTransactionCommand { get; }
        public DelegateCommand GetTransactionsCommand { get; }
        public Action BackToBudget { get; set; }
        public List<TransactionsDetailsViewModel> AllTransactions { get; set; }
        public ObservableCollection<TransactionsDetailsViewModel> Transactions { get; set; }
        public int StartBound 
        {
            get
            {
                return _startBound;
            }
            set 
            {
                _startBound = value;
                _canApplyRange = this[nameof(StartBound)] == string.Empty && this[nameof(EndBound)] == string.Empty;
                GetTransactionsCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(EndBound));
            } 
        }

        public int EndBound 
        {
            get 
            {
                return _endBound;
            }
            set
            {
                _endBound = value;
                _canApplyRange = this[nameof(StartBound)] == string.Empty && this[nameof(EndBound)] == string.Empty;
                GetTransactionsCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(StartBound));
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnable;
            }
            set
            {
                _isEnable = value;
                RaisePropertyChanged();
            }
        }

        public TransactionsDetailsViewModel CurrentTransaction
        {
            get
            {
                return _currentTransaction;
            }
            set
            {
                _currentTransaction = value;
                RaisePropertyChanged();
                DeleteTransactionCommand.RaiseCanExecuteChanged();
            }
        }

        public TransactionsViewModel(BudgetsDetailsViewModel currentBudget,/*Guid budgetId,*/ Action backToBudget)
        {
            CurrentBudget = currentBudget;
            BackToBudget = backToBudget;
            _service = new TransactionsService(CurrentBudget.Guid /*budgetId*/);
            AddTransactionCommand = new DelegateCommand(AddTransaction);
            DeleteTransactionCommand = new DelegateCommand(DeleteTransaction, () => CurrentTransaction != null);
            DeleteAllTracsactionsCommand = new DelegateCommand(DeleteAllTransactions);
            GetTransactionsCommand = new DelegateCommand(GetTransactions, () => _canApplyRange);
            ClearSensitiveData();
            StartBound = 1;
            EndBound = Math.Max(Math.Min(AllTransactions.Count, 10), 1);
            GetTransactions();
            GetCostsOrReceipts();
        }

        private async void DeleteTransaction()
        {
            IsEnabled = false;
            MessageBoxResult boxResult = MessageBox.Show("Are you sure you want to delete your transaction? ", "Remove", MessageBoxButton.YesNo);
            if (boxResult == MessageBoxResult.Yes)
            {
                CurrentBudget.Balance -= CurrentTransaction.Value;
                if (CurrentTransaction.Value > 0)
                {
                    CurrentBudget.Income -= CurrentTransaction.Value;
                }
                else
                {
                    CurrentBudget.Expenses -= CurrentTransaction.Value;
                }
                await _service.DeleteTransaction(CurrentTransaction.Guid);
                int index = Transactions.IndexOf(CurrentTransaction);
                Transactions.RemoveAt(index);
                if (index == Transactions.Count)
                {
                    if (index == 0)
                    {
                        CurrentTransaction = null;
                    }
                    else
                    {
                        CurrentTransaction = Transactions.ElementAt(index - 1);
                    }
                }
                else
                {
                    CurrentTransaction = Transactions.ElementAt(index);
                }
            }
            IsEnabled = true;
            CurrentBudget.UpdateCommand.Execute();
        }

        private async void DeleteAllTransactions()
        {
            await _service.DeleteTransaction(Guid.Empty);
        }

        private async void AddTransaction()
        {
            IsEnabled = false;
            var transaction = new Transaction(Guid.NewGuid(), 0m, Currency.UAH, DateTime.Now);
            await _service.AddOrUpdateAsync(transaction);
            var td = new TransactionsDetailsViewModel(transaction, this, _service);
            AllTransactions.Insert(0, td);
            EndBound = Math.Min(AllTransactions.Count, 10);
            RaisePropertyChanged(nameof(EndBound));
            GetTransactions();
            CurrentTransaction = td;
            IsEnabled = true;
        }

        /*private async void AddBudget()
        {
            IsEnabled = false;
            var wallet = new Budget(Guid.NewGuid(), "DefaultBudget", 0m, CurrentUser.User, BusinessLayer.Currency.UAH);
            await _service.AddOrUpdateAsync(wallet);
            var bd = new BudgetsDetailsViewModel(wallet);
            Budgets.Add(bd);
            CurrentBudget = bd;
            IsEnabled = true;
        }

        private async void DeleteBudget()
        {
            IsEnabled = false;
            MessageBoxResult boxResult =
                MessageBox.Show("Are you sure you want to delete your wallet? ", "Remove", MessageBoxButton.YesNo);
            if (boxResult == MessageBoxResult.Yes)
            {
                await _service.DeleteBudget(CurrentBudget.Guid);
                int index = Budgets.IndexOf(CurrentBudget);
                Budgets.RemoveAt(index);
                if (index == Budgets.Count)
                {
                    if (index == 0)
                    {
                        CurrentBudget = null;
                    }
                    else
                    {
                        CurrentBudget = Budgets.ElementAt(index - 1);
                    }
                }
                else
                {
                    CurrentBudget = Budgets.ElementAt(index);
                }
            }
            IsEnabled = true;
        }*/

        public MainNavigatableTypes Type
        {
            get
            {
                return MainNavigatableTypes.Budget;
            }
        }

        public void GetTransactions()
        {
            Transactions.Clear();
            if (AllTransactions.Count - StartBound < 0)
            {
                return;
            }
            var t = AllTransactions.GetRange(StartBound - 1, Math.Min(AllTransactions.Count - StartBound + 1, EndBound - StartBound + 1));
            foreach (var transaction in t)
            {
                Transactions.Add(transaction);
            }
        }

        public void ClearSensitiveData()
        {
            Transactions = new ObservableCollection<TransactionsDetailsViewModel>();
            AllTransactions = new List<TransactionsDetailsViewModel>();
            var res = Task.Run(_service.GetTransations).Result;
            foreach (var transaction in res)
            {
                AllTransactions.Add(new TransactionsDetailsViewModel(transaction, this, _service));
            }
        }

        private void GetCostsOrReceipts()
        {
            foreach (var transaction in Transactions)
            {
                if (DateTime.Today.Month == transaction.Date.Month)
                {
                    if (transaction.Value > 0)
                    {
                        CurrentBudget.Income += transaction.Value;
                    }
                    else if (transaction.Value < 0)
                    {
                        CurrentBudget.Expenses += transaction.Value;
                    }
                }
            }
        }

        public void SortAllTransactions()
        {
            AllTransactions = AllTransactions.OrderByDescending(transaction => transaction.Date).ToList();
            GetTransactions();
        }

        public string Error { get; }

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "StartBound":
                        if (StartBound < 1)
                        {
                            error = "StartBound cannot be less than 1";
                        }
                        else if (EndBound < StartBound)
                        {
                            error = "StartBound cannot be greater than EndBound";
                        }
                        else if (EndBound - StartBound > 9)
                        {
                            error = "Range cannot be more than 10";
                        }
/*                        else if (StartBound > AllTransactions.Count)
                        {
                            error = "StartBound cannot be greater than len of AllTransactions";
                        }*/
                        break;
                    case "EndBound":
                        if (EndBound < 1)
                        {
                            error = "EndBound cannot be less than 1";
                        }
                        else if (EndBound < StartBound)
                        {
                            error = "EndBound cannot be less than StartBound";
                        }
                        else if (EndBound - StartBound > 9)
                        {
                            error = "Range cannot be more than 10";
                        }
                        break;
                }
                return error;
            }
        }
    }
}
