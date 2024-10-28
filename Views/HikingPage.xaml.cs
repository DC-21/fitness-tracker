using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Fit.Data;
using Fit.Models;

namespace Fit.Views
{
    public partial class HikingPage : Page
    {
        private readonly FitDbContext _context;

        public HikingPage()
        {
            InitializeComponent();
            _context = new FitDbContext();
            Loaded += HikingPage_Loaded;
        }

        private void HikingPage_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("HikingPage Loaded. Loading activities...");
            LoadActivities();
        }

        private void OnAddActivityClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Add Activity button clicked.");

            if (!TryGetUserId(out int userId))
                return;

            if (ValidateInputs(out double distance, out double timeTaken, out double elevationGain, out DateTime activityDate))
            {
                AddActivity(userId, distance, timeTaken, elevationGain, activityDate);
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

        private void AddActivity(int userId, double distance, double timeTaken, double elevationGain, DateTime activityDate)
        {
            try
            {
                // Calculate calories burned (approximation: 0.1 calories per kg per km)
                double weightInKg = 70; // Example weight, replace with actual user weight
                double caloriesBurned = distance * weightInKg * 0.1;

                // Convert local time to UTC
                DateTime utcDate = activityDate.ToUniversalTime();

                var hikingActivity = new Hiking
                {
                    UserId = userId,
                    Distance = distance,
                    TimeTaken = timeTaken,
                    ElevationGain = elevationGain,
                    Date = utcDate,
                    CaloriesBurned = caloriesBurned // Store calories burned
                };

                _context.Hikings.Add(hikingActivity);
                _context.SaveChanges();

                MessageBox.Show("Hiking activity added successfully!");
                Debug.WriteLine($"Added activity: Distance={distance}, TimeTaken={timeTaken}, ElevationGain={elevationGain}, Date={utcDate}, CaloriesBurned={caloriesBurned}");
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
                var activities = _context.Hikings
                    .Where(h => h.UserId == userId)
                    .Select(h => new
                    {
                        h.Date,
                        h.Distance,
                        h.TimeTaken,
                        h.ElevationGain,
                        h.CaloriesBurned // Include calories burned in the results
                    })
                    .ToList();

                HikingDataGrid.ItemsSource = activities;

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
            ElevationGainInput.Clear();
            DateInput.SelectedDate = null;
        }

        private bool ValidateInputs(out double distance, out double timeTaken, out double elevationGain, out DateTime activityDate)
        {
            distance = 0;
            timeTaken = 0;
            elevationGain = 0;
            activityDate = DateTime.MinValue;

            return double.TryParse(DistanceInput.Text, out distance) &&
                   double.TryParse(TimeTakenInput.Text, out timeTaken) &&
                   double.TryParse(ElevationGainInput.Text, out elevationGain) &&
                   DateInput.SelectedDate.HasValue &&
                   (activityDate = DateInput.SelectedDate.Value) > DateTime.MinValue;
        }
    }
}
