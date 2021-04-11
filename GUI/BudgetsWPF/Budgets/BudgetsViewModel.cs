using BudgetsWPF.Navigation;
using Models.Budgets;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace BudgetsWPF.Budgets
{
    public class BudgetsViewModel : BindableBase, INavigatable<MainNavigatableTypes>
    {
        private BudgetsService _service;
        private BudgetsDetailsViewModel _currentBudget;
/*        public DelegateCommand CreateCommand { get; }*/
        public ObservableCollection<BudgetsDetailsViewModel> Budgets { get; set; }

        public BudgetsDetailsViewModel CurrentBudget
        {
            get 
            {
                return _currentBudget;
            }
            set
            {
                _currentBudget = value;
                RaisePropertyChanged();
            }
        }

        public BudgetsViewModel()
        {
/*            CreateCommand = new DelegateCommand();*/
            _service = new BudgetsService();
            Budgets = new ObservableCollection<BudgetsDetailsViewModel>();
            foreach (var budget in _service.GetBudgets())
            {
                Budgets.Add(new BudgetsDetailsViewModel(budget));
            }
        }

        public MainNavigatableTypes Type
        {
            get
            {
                return MainNavigatableTypes.Budget;
            }
        }

        public void ClearSensitiveData()
        {
        }
    }
}
