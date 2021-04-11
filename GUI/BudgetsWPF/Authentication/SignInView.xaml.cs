using System.Windows;
using System.Windows.Controls;

namespace BudgetsWPF.Authentication
{
    /// <summary>
    /// Логика взаимодействия для SingInView.xaml
    /// </summary>
    public partial class SignInView : UserControl
    {
        /*private SignInViewModel _viewModel;*/
        public SignInView(/*Action GoToSignUp, Action GoToBudgetsView*/)
        {
            InitializeComponent();
/*            _viewModel = new SignInViewModel(GoToSignUp, GoToBudgetsView);
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
            ((SignInViewModel)DataContext).Password = TbPassword.Password;
        }
    }
}
