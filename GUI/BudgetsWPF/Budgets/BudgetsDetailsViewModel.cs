using BusinessLayer;
using BusinessLayer.Budgets;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace BudgetsWPF.Budgets
{
    public class BudgetsDetailsViewModel : BindableBase
    {
        private Budget _budget;
        private bool _isEnabled = true;
        private BudgetsService _service;
        private bool _savable = false;
        private decimal _income = 0;
        private decimal _expenses = 0;

        public DelegateCommand UpdateCommand { get; }

        public decimal Income {
            get 
            {
                return _income;
            }
            set 
            {
                _income = value;
                /*UpdateCommand.RaiseCanExecuteChanged();*/
                RaisePropertyChanged();
            }
        }
        public decimal Expenses
        {
            get
            {
                return _expenses;
            }
            set
            {
                _expenses = value;
                /*UpdateCommand.RaiseCanExecuteChanged();*/
                RaisePropertyChanged();
            }
        }

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
                return _budget.Guid;
            }
        }

        public string Name
        {
            get
            {
                return _budget.Name;
            }
            set
            {
                if (_budget.Name != value)
                {
                    _budget.Name = value;
                    _savable = true;
                    UpdateCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged();
                }
            }
        }

        public decimal StartBalance
        {
            get
            {
                return _budget.StartBalance;
            }
            set
            {
                if (_budget.StartBalance != value)
                {
                    _budget.Balance -= _budget.StartBalance;
                    _budget.StartBalance = value;
                    _budget.Balance += _budget.StartBalance;
                    RaisePropertyChanged();
                    _savable = true;
                    UpdateCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public decimal Balance
        {
            get
            {
                return _budget.Balance;
            }
            set
            {
                _budget.Balance = value;
                RaisePropertyChanged();
                UpdateCommand.RaiseCanExecuteChanged();
            }
        }

        public string Description
        {
            get
            {
                return _budget.Description;
            }
            set
            {
                if (_budget.Description != value)
                {
                    _budget.Description = value;
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
                return _budget.Currency;
            }
            set
            {
                if (_budget.Currency != value)
                {
                    _budget.Currency = value;
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
                return $"{_budget.Name} ({_budget.Balance} {_budget.Currency})";
            }
        }

        public BudgetsDetailsViewModel(Budget budget, BudgetsService service)
        {
            _service = service;
            UpdateCommand = new DelegateCommand(SaveBudget, () => _savable);
            _budget = budget;
        }

        private async void SaveBudget()
        {
            IsEnabled = false;
            await _service.AddOrUpdateAsync(_budget);
            _savable = false;
            RaisePropertyChanged(nameof(DisplayName));
            IsEnabled = true;
        }
    }
}
