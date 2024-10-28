using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Fit.Models;
using Fit.Data;
using System.Diagnostics;

namespace Fit.Views
{
    public partial class WalkingPage : Page
    {
        private readonly FitDbContext _context;

        public WalkingPage()
        {
            InitializeComponent();
            _context = new FitDbContext();
            Loaded += WalkingPage_Loaded;
        }

        private void WalkingPage_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("WalkingPage Loaded. Loading activities...");
            LoadActivities();
        }

        private void OnAddActivityClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Add Activity button clicked.");

            if (!TryGetUserId(out int userId))
                return;

            if (ValidateInputs(out int steps, out double distance, out int timeTaken, out DateTime activityDate))
            {
                AddActivity(userId, steps, distance, timeTaken, activityDate);
            }
            else
            {
                MessageBox.Show("Invalid input values.");
                Debug.WriteLine("Invalid input values.");
            }
        }

        private bool TryGetUserId(out int userId)
        {
            userId = 0;

            if (!int.TryParse(UserSession.Instance.Id.ToString(), out userId))
            {
                MessageBox.Show("Invalid user ID.");
                Debug.WriteLine("Invalid user ID.");
                return false;
            }

            return true;
        }

        private void AddActivity(int userId, int steps, double distance, int timeTaken, DateTime activityDate)
        {
            try
            {
                // Convert local time to UTC
                DateTime utcDate = activityDate.ToUniversalTime();

                var walkingActivity = new Walking
                {
                    UserId = userId,
                    Steps = steps,
                    Distance = distance,
                    TimeTaken = timeTaken,
                    Date = utcDate, // Store the UTC date

                    CaloriesBurned = CalculateCalories(steps, distance, timeTaken)
                };

                _context.Walkings.Add(walkingActivity);
                _context.SaveChanges();

                MessageBox.Show("Walking activity added successfully!");
                Debug.WriteLine($"Added activity: Steps={steps}, Distance={distance}, TimeTaken={timeTaken}, Date={utcDate}, CaloriesBurned={walkingActivity.CaloriesBurned}");
                ClearInputs();
                LoadActivities();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding activity: " + ex.Message);
                Debug.WriteLine($"Error adding activity: {ex}");
            }
        }

        private void LoadActivities()
        {
            if (!TryGetUserId(out int userId))
                return;

            try
            {
                var activities = _context.Walkings
                    .Where(w => w.UserId == userId)
                    .Select(w => new
                    {
                        w.Steps,
                        w.Distance,
                        w.TimeTaken,
                        w.Date,
                        w.CaloriesBurned
                    })
                    .ToList();

                ActivitiesDataGrid.ItemsSource = activities;

                if (activities.Any())
                {
                    Debug.WriteLine($"Loaded {activities.Count} activities for user ID {userId}.");
                }
                else
                {
                    MessageBox.Show("No activities found for this user.");
                    Debug.WriteLine($"No activities found for user ID {userId}.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading activities: " + ex.Message);
                Debug.WriteLine($"Error loading activities: {ex}");
            }
        }

        private bool ValidateInputs(out int steps, out double distance, out int timeTaken, out DateTime activityDate)
        {
            steps = 0;
            distance = 0.0;
            timeTaken = 0;
            activityDate = DateTime.MinValue;

            if (!int.TryParse(StepsInput.Text, out steps) || steps <= 0)
            {
                MessageBox.Show("Please enter a valid number of steps (greater than 0).");
                Debug.WriteLine("Invalid steps input.");
                return false;
            }

            if (!double.TryParse(DistanceInput.Text, out distance) || distance <= 0)
            {
                MessageBox.Show("Please enter a valid distance (greater than 0).");
                Debug.WriteLine("Invalid distance input.");
                return false;
            }

            if (!int.TryParse(TimeInput.Text, out timeTaken) || timeTaken < 0)
            {
                MessageBox.Show("Please enter a valid time taken (0 or more minutes).");
                Debug.WriteLine("Invalid time input.");
                return false;
            }

            // Parse the date input
            if (DateInput.SelectedDate == null)
            {
                MessageBox.Show("Please select a valid date.");
                Debug.WriteLine("Invalid date input.");
                return false;
            }
            activityDate = DateInput.SelectedDate.Value; // Get the selected date

            Debug.WriteLine($"Validated inputs: Steps={steps}, Distance={distance}, TimeTaken={timeTaken}, ActivityDate={activityDate}");
            return true;
        }

        private void ClearInputs()
        {
            StepsInput.Clear();
            DistanceInput.Clear();
            TimeInput.Clear();
            DateInput.SelectedDate = null; // Clear the date input
            Debug.WriteLine("Inputs cleared.");
        }

        private double CalculateCalories(int steps, double distance, int timeTaken)
        {
            double caloriesFromSteps = steps * 0.04;
            double caloriesFromDistance = distance * 0.1;
            double caloriesFromTime = timeTaken * 0.5;

            double totalCalories = caloriesFromSteps + caloriesFromDistance + caloriesFromTime;

            Debug.WriteLine($"Calculated calories burned: {totalCalories}");
            return totalCalories;
        }
    }
}
