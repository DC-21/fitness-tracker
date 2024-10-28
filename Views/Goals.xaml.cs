using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Fit.Data;
using Fit.Models;
using Microsoft.EntityFrameworkCore;

namespace Fit.Views
{
    public partial class Goals : Page
    {
        private readonly FitDbContext _context;

        public Goals()
        {
            InitializeComponent();
            _context = new FitDbContext();
            Loaded += Goals_Loaded; // Subscribe to the Loaded event
        }

        private void Goals_Loaded(object sender, RoutedEventArgs e)
        {
            LoadUserGoals(); // Load goals when the page is loaded
        }

        private void LoadUserGoals()
        {
            if (!TryGetUserId(out int userId))
                return;

            var userGoals = _context.Goals
                .Where(g => g.UserId == userId)
                .Select(g => new
                {
                    g.Id,
                    g.Name,
                    g.Description,
                    g.CalorieTarget,
                    g.StartDate,
                    g.EndDate,
                    g.IsAchieved
                })
                .ToList();

            GoalsDataGrid.ItemsSource = userGoals;

            if (userGoals.Any())
            {
                Debug.WriteLine($"Loaded {userGoals.Count} goals for user ID {userId}.");
            }
            else
            {
                MessageBox.Show("No goals found for this user.");
                Debug.WriteLine($"No goals found for user ID {userId}.");
            }
        }

        private bool TryGetUserId(out int userId)
        {
            userId = 0;

            // Replace with your actual user session management
            if (!int.TryParse(UserSession.Instance.Id.ToString(), out userId))
            {
                MessageBox.Show("Invalid user ID.");
                Debug.WriteLine("Invalid user ID.");
                return false;
            }

            return true;
        }

        private void AddGoalButton_Click(object sender, RoutedEventArgs e)
        {
            string description = GoalDescriptionTextBox.Text;
            string calorieTargetText = CalorieTargetTextBox.Text;
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;
            var selectedActivityItem = ActivityComboBox.SelectedItem as ComboBoxItem;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(description) || selectedActivityItem == null)
            {
                MessageBox.Show("Please enter all required fields.");
                return;
            }

            if (!double.TryParse(calorieTargetText, out double calorieTarget) || calorieTarget <= 0)
            {
                MessageBox.Show("Please enter a valid calorie target.");
                return;
            }

            if (startDate == null || endDate == null || startDate > endDate)
            {
                MessageBox.Show("Please select valid start and end dates.");
                return;
            }

            // Convert the selected dates to UTC
            DateTime startDateUtc = DateTime.SpecifyKind(startDate.Value, DateTimeKind.Utc);
            DateTime endDateUtc = DateTime.SpecifyKind(endDate.Value, DateTimeKind.Utc);

            // Get the selected activity name
            string selectedActivityName = selectedActivityItem.Content.ToString();

            // Get the user ID from the session
            if (!TryGetUserId(out int userId))
                return;

            // Create the new Goal
            var goal = new Goal
            {
                Description = description,
                CalorieTarget = calorieTarget,
                StartDate = startDateUtc,
                EndDate = endDateUtc,
                Name = selectedActivityName,
                UserId = userId // Set the UserId here
            };

            _context.Goals.Add(goal);
            try
            {
                _context.SaveChanges();
                MessageBox.Show("Goal added successfully!");
                LoadUserGoals(); // Refresh the goals list after adding a new goal
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"An error occurred while saving the goal: {ex.Message}");
            }
        }

        private void MarkGoalAsAchieved(int goalId)
        {
            var goal = _context.Goals.Find(goalId);
            if (goal != null)
            {
                goal.IsAchieved = true;

                try
                {
                    _context.SaveChanges();
                    MessageBox.Show("Goal marked as achieved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while updating the goal: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                LoadUserGoals(); // Refresh the goals list after marking as achieved
            }
            else
            {
                MessageBox.Show("Goal not found or already marked as achieved.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void MarkAsAchievedButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button != null && button.DataContext is Goal goal)
            {
                // Show a confirmation message before marking as achieved
                var result = MessageBox.Show("Are you sure you want to mark this goal as achieved?",
                                             "Confirm",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    MarkGoalAsAchieved(goal.Id); // Call the method to mark the goal as achieved
                }
            }
            else
            {
                MessageBox.Show("Failed to retrieve goal information. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
