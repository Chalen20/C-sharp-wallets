using BudgetsWPF.Budgets;
using BusinessLayer;
using BusinessLayer.Transactions;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace BudgetsWPF.Transactions
{
    public class TransactionsDetailsViewModel : BindableBase
    {
        private Transaction _transaction;
        private bool _isEnabled = true;
        private TransactionsService _service;
        private bool _savable = false;
        private decimal _sTransactionValue;
        private Action _sortAllTransactions;
        private bool _isSortable = false;
        public BudgetsDetailsViewModel CurrentBudget { get;  }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                RaisePropertyChanged();
            }
        }
        public Guid Guid
        {
            get
            {
                return _transaction.Guid;
            }
        }

        public DelegateCommand UpdateCommand { get; }
        public DelegateCommand BackToBudgetCommand { get; }

        public DateTime Date
        {
            get
            {
                return _transaction.Date;
            }
            set
            {
                if (_transaction.Date != value)
                {
                    _isSortable = true;
                    _transaction.Date = value;
                    _savable = true;
                    UpdateCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged();
                }
            }
        }

        public decimal Value
        {
            get
            {
                return _transaction.Value;
            }
            set
            {
                if (_transaction.Value != value)
                {
                    _transaction.Value = value;
                    RaisePropertyChanged();
                    _savable = true;
                    UpdateCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Description
        {
            get
            {
                return _transaction.Description;
            }
            set
            {
                if (_transaction.Description != value)
                {
                    _transaction.Description = value;
                    RaisePropertyChanged();
                    _savable = true;
                    UpdateCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public Currency Currency
        {
            get
            {
                return _transaction.Currency;
            }
            set
            {
                if (_transaction.Currency != value)
                {
                    _transaction.Currency = value;
                    RaisePropertyChanged();
                    _savable = true;
                    UpdateCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string DisplayName
        {
            get
            {
                return $"{_transaction.Date} ({_transaction.Value} {_transaction.Currency})";
            }
        }

        public TransactionsDetailsViewModel(Transaction transaction, TransactionsViewModel tvm, TransactionsService service)
        {
            _sortAllTransactions = tvm.SortAllTransactions;
            CurrentBudget = tvm.CurrentBudget;
            _service = service;
            UpdateCommand = new DelegateCommand(SaveTransaction, () => _savable);
            BackToBudgetCommand = new DelegateCommand(tvm.BackToBudget);
            _transaction = transaction;
            _sTransactionValue = _transaction.Value;
        }

        private async void SaveTransaction()
        {
            IsEnabled = false;
            await _service.AddOrUpdateAsync(_transaction);
            _savable = false;
            RaisePropertyChanged(nameof(DisplayName));
            UpdateCommand.RaiseCanExecuteChanged();
            CurrentBudget.UpdateCommand.Execute();
            IsEnabled = true;
            CurrentBudget.Balance -= _sTransactionValue;
            CurrentBudget.Balance += _transaction.Value;
            if (_transaction.Date != DateTime.Now)
            {
                if (_transaction.Value >= 0 && _sTransactionValue >= 0)
                {
                    CurrentBudget.Income -= _sTransactionValue;
                    CurrentBudget.Income += _transaction.Value;
                }
                else if (_transaction.Value > 0 && _sTransactionValue < 0)
                {
                    CurrentBudget.Income += _transaction.Value;
                    CurrentBudget.Expenses -= _sTransactionValue;
                }
                else if (_transaction.Value < 0 && _sTransactionValue > 0)
                {
                    CurrentBudget.Income -= _sTransactionValue;
                    CurrentBudget.Expenses += _transaction.Value;
                }
                else
                {
                    CurrentBudget.Expenses -= _sTransactionValue;
                    CurrentBudget.Expenses += _transaction.Value;
                }
            }
            _sTransactionValue = _transaction.Value;
            if (_isSortable)
            {
                _sortAllTransactions.Invoke();
            }
        }
    }
}
