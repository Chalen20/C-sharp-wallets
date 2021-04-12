using BudgetsWPF.Navigation;
using BusinessLayer.Budgets;
using BusinessLayer.Users;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BudgetsWPF.Budgets
{
    public class BudgetsViewModel : BindableBase, INavigatable<MainNavigatableTypes>
    {
        private BudgetsService _service;
        private BudgetsDetailsViewModel _currentBudget;
        private bool _isEnable = true;
        public DelegateCommand CreateCommand { get; }
        public DelegateCommand DeleteCommand { get; }
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
        
        public BudgetsViewModel()
        {
            _service = new BudgetsService();
            CreateCommand = new DelegateCommand(AddBudget);
            DeleteCommand = new DelegateCommand(DeleteBudget/* () => CurrentBudget != null*/);
            Budgets = new ObservableCollection<BudgetsDetailsViewModel>();
            var res = Task.Run(_service.GetBudgets).Result;
            foreach (var budget in res)
            {
                Budgets.Add(new BudgetsDetailsViewModel(budget));
            }
        }

        private async void AddBudget()
        {
            IsEnabled = false;
            var wallet = new Budget(Guid.NewGuid(), "DefaultBudget", 0m, CurrentUser.User, BusinessLayer.Currency.UAH);
            await _service.AddOrUpdateAsync(wallet);
            var bd = new BudgetsDetailsViewModel(wallet);
            Budgets.Add(bd);
            CurrentBudget = bd;
            IsEnabled = true;
        }

        private void DeleteBudget()
        {
            IsEnabled = false;
            MessageBoxResult boxResult =
                MessageBox.Show("Are you sure you want to delete your wallet? ", "Remove", MessageBoxButton.YesNo);
            if (boxResult == MessageBoxResult.Yes)
            {
                _service.DeleteBudget(CurrentBudget.Guid);
                int index = Budgets.IndexOf(CurrentBudget);
                Budgets.RemoveAt(index);
                CurrentBudget = index == Budgets.Count
                    ? index == 0 ? null : Budgets.ElementAt(index - 1)
                    : Budgets.ElementAt(index);
            }
            IsEnabled = true;
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
