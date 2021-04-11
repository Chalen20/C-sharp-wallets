using System.Windows.Controls;

namespace BudgetsWPF
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            /*Content = new AuthView(GoToBudgetsView);*/
        }

/* public void GoToSignUp()
        {
            Content = new SingUpView(GoToSignIn);
        }

        public void GoToSignIn()
        {
            Content = new SingInView(GoToSignUp, GoToBudgetsView);
        }*/

/*        public void GoToBudgetsView()
        {
            Content = new Budgets.BudgetsView();
        }*/
    }
}
