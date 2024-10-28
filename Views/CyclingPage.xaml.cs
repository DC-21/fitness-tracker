using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Fit.Data;
using Fit.Models;

namespace Fit.Views
{
    public partial class CyclingPage : Page
    {
        private readonly FitDbContext _context;

        public CyclingPage()
        {
            InitializeComponent();
            _context = new FitDbContext();
            Loaded += CyclingPage_Loaded;
        }

        private void CyclingPage_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("CyclingPage Loaded. Loading activities...");
            LoadActivities();
        }

        private void OnAddActivityClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Add Activity button clicked.");

            if (!TryGetUserId(out int userId))
                return;

            if (ValidateInputs(out double distance, out double timeTaken, out double averageSpeed, out DateTime activityDate))
            {
                AddActivity(userId, distance, timeTaken, averageSpeed, activityDate);
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

            // Replace with your actual user session management
            if (!int.TryParse(UserSession.Instance.Id.ToString(), out userId))
            {
                MessageBox.Show("Invalid user ID.");
                Debug.WriteLine("Invalid user ID.");
                return false;
            }

            return true;
        }


        private void AddActivity(int userId, double distance, double timeTaken, double averageSpeed, DateTime activityDate)
        {
            try
            {
                // Convert local time to UTC
                DateTime utcDate = activityDate.ToUniversalTime();

                // Assuming a MET value for cycling (varies by intensity)
                double metValue = 8.0; // Example MET value for cycling
                double userWeight = 70.0; // Example user weight in kg
                double durationInHours = timeTaken / 60.0; // Convert minutes to hours

                // Calculate calories burned
                double caloriesBurned = metValue * userWeight * durationInHours;

                var cyclingActivity = new Cycling
                {
                    UserId = userId,
                    Distance = distance,
                    TimeTaken = timeTaken,
                    AverageSpeed = averageSpeed,
                    Date = utcDate,
                    CaloriesBurned = caloriesBurned // Store the calculated calories
                };

                _context.Cyclings.Add(cyclingActivity);
                _context.SaveChanges();

                MessageBox.Show("Cycling activity added successfully!");
                Debug.WriteLine($"Added activity: Distance={distance}, TimeTaken={timeTaken}, AverageSpeed={averageSpeed}, CaloriesBurned={caloriesBurned}, Date={utcDate}");
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
                var activities = _context.Cyclings
                    .Where(c => c.UserId == userId)
                    .Select(c => new
                    {
                        c.Date,
                        c.Distance,
                        c.TimeTaken,
                        c.AverageSpeed,
                        c.CaloriesBurned
                    })
                    .ToList();

                CyclingDataGrid.ItemsSource = activities;

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

        private void ClearInputs()
        {
            DistanceInput.Clear();
            TimeTakenInput.Clear();
            AverageSpeedInput.Clear();
            DateInput.SelectedDate = null;
        }

        private bool ValidateInputs(out double distance, out double timeTaken, out double averageSpeed, out DateTime activityDate)
        {
            distance = 0;
            timeTaken = 0;
            averageSpeed = 0;
            activityDate = DateTime.MinValue;

            return double.TryParse(DistanceInput.Text, out distance) &&
                   double.TryParse(TimeTakenInput.Text, out timeTaken) &&
                   double.TryParse(AverageSpeedInput.Text, out averageSpeed) &&
                   DateInput.SelectedDate.HasValue &&
                   (activityDate = DateInput.SelectedDate.Value) > DateTime.MinValue;
        }
    }
}
