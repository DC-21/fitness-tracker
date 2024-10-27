using System.Windows;
using System.Windows.Controls;

namespace Fit
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Navigate to Login page on startup
            MainFrame.Navigate(new Views.Login());
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Login page
            MainFrame.Navigate(new Views.Login());
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Register page
            MainFrame.Navigate(new Views.Register());
        }

        private void MainFrame_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            // Hide the welcome panel when navigating to another page
            WelcomePanel.Visibility = Visibility.Collapsed;
        }

        private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            // Optionally, you can handle any actions after navigation here
        }
    }
}
