using Models.Budgets;
using Prism.Commands;
using Prism.Mvvm;

namespace BudgetsWPF.Budgets
{
    public class BudgetsDetailsViewModel : BindableBase
    {
        private Budget _budget;

        public DelegateCommand UpdateCommand { get; }
        public DelegateCommand DeleteCommand { get; }

        public string Name
        {
            get
            {
                return _budget.Name;
            }
            set
            {
                _budget.Name = value;
                RaisePropertyChanged(nameof(DisplayName));
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
                RaisePropertyChanged(nameof(DisplayName));
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
                _budget.Description = value;
                /*RaisePropertyChanged(nameof(DisplayName));*/
            }
        }

        public string DisplayName
        {
            get
            {
                return $"{_budget.Name} (${_budget.Balance})";
            }
        }

        public BudgetsDetailsViewModel(Budget budget)
        {
/*            UpdateCommand = new DelegateCommand();*/
            _budget = budget;
        }
    }
}
