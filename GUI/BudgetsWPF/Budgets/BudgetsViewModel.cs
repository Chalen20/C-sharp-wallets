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

        public DelegateCommand SignOutCommand { get; }
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
                DeleteCommand.RaiseCanExecuteChanged();
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
        
        public BudgetsViewModel(Action SignOut)
        {
            CreateCommand = new DelegateCommand(AddBudget);
            DeleteCommand = new DelegateCommand(DeleteBudget, () => CurrentBudget != null);
            SignOutCommand = new DelegateCommand(SignOut);
            ClearSensitiveData();
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
            _service = new BudgetsService();
            Budgets = new ObservableCollection<BudgetsDetailsViewModel>();
            CurrentBudget = null;
            var res = Task.Run(_service.GetBudgets).Result;
            foreach (var budget in res)
            {
                Budgets.Add(new BudgetsDetailsViewModel(budget));
            }
        }
    }
}
