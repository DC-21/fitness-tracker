using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Fit.Data;
using Fit.Models;

namespace Fit.Views
{
    public partial class SwimmingPage : Page
    {
        private readonly FitDbContext _context;

        public SwimmingPage()
        {
            InitializeComponent();
            _context = new FitDbContext();
            Loaded += SwimmingPage_Loaded;
        }

        private void SwimmingPage_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("SwimmingPage Loaded. Loading activities...");
            LoadActivities();
        }

        private void OnAddActivityClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Add Activity button clicked.");

            if (!TryGetUserId(out int userId))
                return;

            if (ValidateInputs(out int laps, out int timeTaken, out double averageHeartRate, out DateTime activityDate))
            {
                // Calculate calories burned
                double caloriesBurned = CalculateCaloriesBurned(laps, timeTaken, averageHeartRate);
                AddActivity(userId, laps, timeTaken, averageHeartRate, caloriesBurned, activityDate);
            }
            else
            {
                MessageBox.Show("Invalid input values.");
                Debug.WriteLine("Invalid input values.");
            }
        }

        private double CalculateCaloriesBurned(int laps, int timeTaken, double averageHeartRate)
        {
            // Example calculation:
            // Assume 0.1 calories burned per lap, 0.5 calories per minute per bpm of average heart rate
            double caloriesFromLaps = laps * 0.1; // Change this multiplier based on actual data
            double caloriesFromHeartRate = (timeTaken * averageHeartRate) * 0.5; // Change this based on actual data

            return caloriesFromLaps + caloriesFromHeartRate; // Total calories burned
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

        private void AddActivity(int userId, int laps, int timeTaken, double averageHeartRate, double caloriesBurned, DateTime activityDate)
        {
            try
            {
                // Convert local time to UTC
                DateTime utcDate = activityDate.ToUniversalTime();

                var swimmingActivity = new Swimming
                {
                    UserId = userId,
                    Laps = laps,
                    TimeTaken = timeTaken,
                    AverageHeartRate = averageHeartRate,
                    CaloriesBurned = caloriesBurned,
                    Date = utcDate // Store the UTC date
                };

                _context.Swimmings.Add(swimmingActivity);
                _context.SaveChanges();

                MessageBox.Show("Swimming activity added successfully!");
                Debug.WriteLine($"Added activity: Laps={laps}, TimeTaken={timeTaken}, AverageHeartRate={averageHeartRate}, Date={utcDate}, CaloriesBurned={caloriesBurned}");
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
                var activities = _context.Swimmings
                    .Where(s => s.UserId == userId)
                    .Select(s => new
                    {
                        s.Date,
                        s.Laps,
                        s.TimeTaken,
                        s.AverageHeartRate,
                        s.CaloriesBurned
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

        private bool ValidateInputs(out int laps, out int timeTaken, out double averageHeartRate, out DateTime activityDate)
        {
            laps = 0;
            timeTaken = 0;
            averageHeartRate = 0.0;
            activityDate = DateTime.MinValue;

            if (!int.TryParse(LapsInput.Text, out laps) || laps <= 0)
            {
                MessageBox.Show("Please enter a valid number of laps (greater than 0).");
                Debug.WriteLine("Invalid laps input.");
                return false;
            }

            if (!int.TryParse(TimeInput.Text, out timeTaken) || timeTaken < 0)
            {
                MessageBox.Show("Please enter a valid time taken (0 or more minutes).");
                Debug.WriteLine("Invalid time input.");
                return false;
            }

            if (!double.TryParse(HeartRateInput.Text, out averageHeartRate) || averageHeartRate <= 0)
            {
                MessageBox.Show("Please enter a valid average heart rate (greater than 0).");
                Debug.WriteLine("Invalid average heart rate input.");
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

            Debug.WriteLine($"Validated inputs: Laps={laps}, TimeTaken={timeTaken}, AverageHeartRate={averageHeartRate}, ActivityDate={activityDate}");
            return true;
        }

        private void ClearInputs()
        {
            LapsInput.Clear();
            TimeInput.Clear();
            HeartRateInput.Clear();
            DateInput.SelectedDate = null; // Clear the date input
            Debug.WriteLine("Inputs cleared.");
        }
    }
}
