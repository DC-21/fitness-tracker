using Fit.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Fit.Views
{
    /// <summary>
    /// Interaction logic for Topbar.xaml
    /// </summary>
    public partial class Topbar : UserControl
    {
        public Topbar()
        {
            InitializeComponent();
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            // Access the full name from the UserSession and display it
            UserNameTextBlock.Text = UserSession.Instance.UserName;
        }

        // Navigation event handlers
        private void Home_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigateToView("Dashboard");
        }

        private void Goals_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigateToView("Goals");
        }

        private void Hiking_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigateToView("Hiking");
        }

        private void Weights_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigateToView("Weights");
        }

        private void Swimming_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigateToView("Swimming");
        }

        private void Walking_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigateToView("WalkingPage");
        }

        private void Running_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigateToView("Running");
        }

        private void Cycling_MouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigateToView("Cycling");
        }

        private void LogoutTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Clear user session data on logout
            UserSession.Instance.ClearUserData();
            MessageBox.Show("You have logged out.");
            (Application.Current.MainWindow as MainWindow)?.MainFrame.Navigate(new Login());
        }

        private void NavigateToView(string viewName)
        {
            var view = GetViewByName(viewName);
            if (view != null)
            {
                (Application.Current.MainWindow as MainWindow)?.MainFrame.Navigate(view);
            }
            else
            {
                MessageBox.Show($"View '{viewName}' not found.");
            }
        }

        // Method to retrieve views by name
        private Page GetViewByName(string viewName)
        {
            switch (viewName)
            {
                case "Dashboard":
                    return new Dashboard();
                case "Goals":
                    return new Goals();
                case "Hiking":
                    return new HikingPage();
                case "Weights":
                    return new WeightsPage();
                case "Swimming":
                    return new SwimmingPage();
                case "WalkingPage":
                    return new WalkingPage();
                case "Running":
                    return new RunningPage();
                case "Cycling":
                    return new CyclingPage();
                default:
                    return null;
            }
        }
    }
}
