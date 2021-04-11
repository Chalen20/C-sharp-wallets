using System.Windows;
using System.Windows.Controls;

namespace BudgetsWPF.Authentication
{
    /// <summary>
    /// Логика взаимодействия для SingInView.xaml
    /// </summary>
    public partial class SignUpView : UserControl
    {
/*        private SignUpViewModel _viewModel;*/
        public SignUpView(/*Action GoToSignIn*/)
        {
            InitializeComponent();
/*            _viewModel = new SignUpViewModel(GoToSignIn);
            this.DataContext = _viewModel;*/
            // BSignIn.IsEnabled = false;
        }

        /*        private void BClose_Click(object sender, RoutedEventArgs e)
                {
                    Environment.Exit(0);
                }*/

        /*        private void BSignIn_Click(object sender, RoutedEventArgs e)
                {
                    if (String.IsNullOrWhiteSpace(TbLogin.Text) || String.IsNullOrWhiteSpace(TbPassword.Text))
                    {
                        MessageBox.Show("Login and/or password is empty");
                    }
                    else
                    {
                        var authUser = new AuthenticationUser()
                        {
                            Login = TbLogin.Text,
                            Password = TbPassword.Text,
                        };
                        var authService = new AuthenticationService();
                        User user = null;
                        try
                        {
                            user = authService.Authenticate(authUser);
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show($"Sign in failed: {ex.Message}");
                            return;
                        }
                        MessageBox.Show($"Sign in was successful for user {user.FirstName} {user}");
                    }
                }*/

        private void TbPassword_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((SignUpViewModel)DataContext).Password = TbPassword.Password;
        }
    }
}
