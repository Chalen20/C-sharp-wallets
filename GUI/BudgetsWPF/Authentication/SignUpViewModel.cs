using BudgetsWPF.Navigation;
using BusinessLayer.Users;
using Prism.Commands;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;

namespace BudgetsWPF.Authentication
{
    public class SignUpViewModel : INotifyPropertyChanged, INavigatable<AuthNavigatableTypes>
    {
        private RegistrationUser _regUser = new RegistrationUser();

        public event PropertyChangedEventHandler PropertyChanged;

        private Action _goToSignIn;

        private bool _isEnabled = true;

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
                return _regUser.Login;
            }
            set 
            {
                if (_regUser.Login != value)
                {
                    _regUser.Login = value;
                    OnPropertyChanged(); //nameof(Login));
                    SignUpCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Password 
        {
            get
            {
                return _regUser.Password;
            }
            set 
            {
                if (_regUser.Password != value)
                {
                    _regUser.Password = value;
                    OnPropertyChanged(); //nameof(Password));
                    SignUpCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string FirstName
        {
            get
            {
                return _regUser.FirstName;
            }
            set
            {
                if (_regUser.FirstName != value)
                {
                    _regUser.FirstName = value;
                    OnPropertyChanged(); //nameof(FirstName));
                    SignUpCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string LastName
        {
            get
            {
                return _regUser.LastName;
            }
            set
            {
                if (_regUser.LastName != value)
                {
                    _regUser.LastName = value;
                    OnPropertyChanged(); //nameof(LastName));
                    SignUpCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public string Email
        {
            get
            {
                return _regUser.Email;
            }
            set
            {
                if (_regUser.Email != value)
                {
                    _regUser.Email = value;
                    OnPropertyChanged(); //nameof(Email));
                    SignUpCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public DelegateCommand SignUpCommand { get; }
        public DelegateCommand CloseCommand { get; }
        public DelegateCommand SignInCommand { get; }

        public AuthNavigatableTypes Type
        {
            get
            {
                return AuthNavigatableTypes.SignUp;
            }
        }
        public SignUpViewModel(Action GoToSignIn) 
        {
            SignUpCommand = new DelegateCommand(SignUp, IsSignUpEnabled);
            CloseCommand = new DelegateCommand(() => Environment.Exit(0));
            _goToSignIn = GoToSignIn;
            SignInCommand = new DelegateCommand(_goToSignIn);
        }

        private bool IsSignUpEnabled()
        {
            return (!String.IsNullOrWhiteSpace(Login) && (!String.IsNullOrWhiteSpace(Password)) && 
                (!String.IsNullOrWhiteSpace(Email)));
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();
                    string domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private async void SignUp()
        {
            if (Login.Length < 5)
                MessageBox.Show("Login is too short(min 5)");
            else if (Password.Length < 6)
                MessageBox.Show("Password is too short(min 6)");
            else if (!IsValidEmail(Email))
                MessageBox.Show("Invalid email");
            else
            {
                var authService = new AuthenticationService();
                try
                {
                    IsEnabled = false;
                    await authService.RegisterUser(_regUser);
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
                MessageBox.Show($"User successfully registered, please Sign In.");
                _goToSignIn.Invoke();
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ClearSensitiveData()
        {
            _regUser = new RegistrationUser();
        }
    }
}
