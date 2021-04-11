using BudgetsWPF.Navigation;
using System;

namespace BudgetsWPF.Authentication
{
    public class AuthViewModel :  NavigationBase<AuthNavigatableTypes>, INavigatable<MainNavigatableTypes>
    {
        private Action _signInSuccess;

        public MainNavigatableTypes Type
        {
            get
            {
                return MainNavigatableTypes.Auth;
            }
        }

        public AuthViewModel(Action SignInSuccess)
        {
            _signInSuccess = SignInSuccess;
            Navigate(AuthNavigatableTypes.SignIn);
        }

        protected override INavigatable<AuthNavigatableTypes> CreateViewModel(AuthNavigatableTypes type)
        {
            if (type == AuthNavigatableTypes.SignIn)
            {
                return new SignInViewModel(() => Navigate(AuthNavigatableTypes.SignUp), _signInSuccess);
            }
            else /*if (type == AuthNavigatableTypes.SignUp)*/
            {
                return new SignUpViewModel(() => Navigate(AuthNavigatableTypes.SignIn));
            }
        }

        public void ClearSensitiveData()
        {
            CurrentViewModel.ClearSensitiveData();
        }
    }
}
