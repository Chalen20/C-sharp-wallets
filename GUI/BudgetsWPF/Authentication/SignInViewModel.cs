using BudgetsWPF.Navigation;
using BusinessLayer.Users;
using Prism.Commands;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace BudgetsWPF.Authentication
{
    public class SignInViewModel : INotifyPropertyChanged, INavigatable<AuthNavigatableTypes>
    {
        private AuthenticationUser _authUser = new AuthenticationUser();
        private Action _goToSignUp;
        private Action _goToBudgets;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool _isEnabled = true;

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                OnPropertyChanged();
            }
        }

        public string Login 
        {
            get 
            {
                return _authUser.Login;
            }
            set 
            {
                if (_authUser.Login != value)
                {
                    _authUser.Login = value;
                    OnPropertyChanged(); //nameof(Login));
                    SignInCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Password 
        {
            get
            {
                return _authUser.Password;
            }
            set 
            {
                if (_authUser.Password != value)
                {
                    _authUser.Password = value;
                    OnPropertyChanged(); //nameof(Password));
                    SignInCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public DelegateCommand SignInCommand { get; }
        public DelegateCommand CloseCommand { get; }
        public DelegateCommand SignUpCommand { get; }

        public AuthNavigatableTypes Type
        {
            get
            {
                return AuthNavigatableTypes.SignIn;
            }
        }

        public SignInViewModel(Action GoToSignUp, Action GoToBudgets) 
        {
            SignInCommand = new DelegateCommand(SignIn, IsSignInEnabled);
            CloseCommand = new DelegateCommand(() => Environment.Exit(0));
            _goToSignUp = GoToSignUp;
            SignUpCommand = new DelegateCommand(_goToSignUp);
            _goToBudgets = GoToBudgets;
        }

        private bool IsSignInEnabled()
        {
            return (!String.IsNullOrWhiteSpace(Login) && (!String.IsNullOrWhiteSpace(Password)));
        }

        private async void SignIn()
        {
            if (String.IsNullOrWhiteSpace(Login) || String.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Login and/or password is empty.");
            }
            else if (Login.Length < 5)
            {
                MessageBox.Show("Login is too short(min 5)");
            }
            else if (Password.Length < 6)
            {
                MessageBox.Show("Password is too short(min 6)");
            }
            else
            {
                var authService = new AuthenticationService();
                User user = null;
                try
                {
                    IsEnabled = false;
                    user = await Task.Run(() => authService.Authenticate(_authUser));
                    CurrentUser.User = user;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sign in failed: {ex.Message}");
                    return;
                }
                finally
                {
                    IsEnabled = true;
                }
                MessageBox.Show($"Sign in was successful for user {user.FirstName} {user.LastName}");
                _goToBudgets.Invoke();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ClearSensitiveData()
        {
            Password = "";
        }
    }
}
