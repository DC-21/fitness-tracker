using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Fit.Data;
using Fit.Models;
using BCrypt.Net;

namespace Fit.Views
{
    public partial class Register : UserControl
    {
        private readonly FitDbContext _context;

        public Register()
        {
            InitializeComponent();
            _context = new FitDbContext();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string email = EmailTextBox.Text;
            string userName = UserNameTextBox.Text;
            string password = PasswordBox.Password;

            // Validate UserName
            if (!IsValidUserName(userName))
            {
                MessageBox.Show("Username can only contain letters and numbers.");
                return;
            }

            // Validate Password
            if (!IsValidPassword(password))
            {
                MessageBox.Show("Password must be at least 12 characters long and contain at least one uppercase letter and one lowercase letter.");
                return;
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Email = email,
                UserName = userName,
                Password = hashedPassword
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            MessageBox.Show("Registration successful!");

            // Navigate to the Login view
            (Application.Current.MainWindow as MainWindow)?.MainFrame.Navigate(new Login());
        }

        private bool IsValidUserName(string userName)
        {
            // Regex to check if username contains only letters and numbers
            return Regex.IsMatch(userName, "^[a-zA-Z0-9]*$");
        }

        private bool IsValidPassword(string password)
        {
            // Check password length and for at least one upper and one lower case letter
            return password.Length >= 12 &&
                   Regex.IsMatch(password, @"[A-Z]") &&
                   Regex.IsMatch(password, @"[a-z]");
        }

        private void Login_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            (Application.Current.MainWindow as MainWindow)?.MainFrame.Navigate(new Login());
        }
    }
}
