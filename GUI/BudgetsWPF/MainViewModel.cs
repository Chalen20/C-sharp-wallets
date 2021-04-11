using BudgetsWPF.Authentication;
using BudgetsWPF.Budgets;
using BudgetsWPF.Navigation;

namespace BudgetsWPF
{
    public class MainViewModel : NavigationBase<MainNavigatableTypes>
    {
        public MainViewModel()
        {
            Navigate(MainNavigatableTypes.Auth);
        }

        protected override INavigatable<MainNavigatableTypes> CreateViewModel(MainNavigatableTypes type)
        {
            if (type == MainNavigatableTypes.Auth)
            {
                return new AuthViewModel(() => Navigate(MainNavigatableTypes.Budget));
            }
            else /*if (type == AuthNavigatableTypes.SignUp)*/
            {
                return new BudgetsViewModel();
            }
        }
    }
}
