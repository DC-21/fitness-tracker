using Fit.Data;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace Fit.Views
{
    public partial class Login : UserControl
    {
        private readonly FitDbContext _context;

        public Login()
        {
            InitializeComponent();
            _context = new FitDbContext();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = UserNameTextBox.Text;
            string password = PasswordBox.Password;

            // Retrieve the user from the database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
            {
                MessageBox.Show("Invalid username or password");
                return;
            }

            // Verify the password
            if (BCrypt.Net.BCrypt.Verify(password, user.Password))
            {

                UserSession.Instance.SetUserData(user.UserName, user.Email, user.Id.ToString());

                MessageBox.Show("Login successful!");

                // Navigate to the dashboard
                var dashboard = new Dashboard(); // Assuming you have a Dashboard view
                ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(dashboard);
            }
            else
            {
                MessageBox.Show("Invalid username or password");
            }
        }

        private void Register_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Navigate back to the registration page
            ((MainWindow)Application.Current.MainWindow).MainFrame.Navigate(new Register());
        }
    }
}
