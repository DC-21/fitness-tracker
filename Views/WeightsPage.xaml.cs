using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Fit.Data;
using Fit.Models;

namespace Fit.Views
{
    public partial class WeightsPage : Page
    {
        private readonly FitDbContext _context;

        public WeightsPage()
        {
            InitializeComponent();
            _context = new FitDbContext();
            Loaded += WeightsPage_Loaded;
        }

        private void WeightsPage_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("WeightsPage Loaded. Loading activities...");
            LoadActivities();
        }

        private void OnAddActivityClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Add Activity button clicked.");

            if (!TryGetUserId(out int userId))
                return;

            if (ValidateInputs(out int sets, out int repetitions, out double weightLifted, out DateTime activityDate))
            {
                // Calculate calories burned
                double caloriesBurned = CalculateCaloriesBurned(sets, repetitions, weightLifted);
                AddActivity(userId, sets, repetitions, weightLifted, caloriesBurned, activityDate);
            }
            else
            {
                MessageBox.Show("Invalid input values.");
                Debug.WriteLine("Invalid input values.");
            }
        }

        private double CalculateCaloriesBurned(int sets, int repetitions, double weightLifted)
        {
            // Example calculation:
            // Assume 0.1 calories burned per repetition per kg lifted
            return sets * repetitions * weightLifted * 0.1; // Adjust the multiplier based on actual data
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

        private void AddActivity(int userId, int sets, int repetitions, double weightLifted, double caloriesBurned, DateTime activityDate)
        {
            try
            {
                // Convert local time to UTC
                DateTime utcDate = activityDate.ToUniversalTime();

                var weightsActivity = new Weights
                {
                    UserId = userId,
                    Sets = sets,
                    Repetitions = repetitions,
                    WeightLifted = weightLifted,
                    CaloriesBurned = caloriesBurned,
                    Date = utcDate // Store the UTC date
                };

                _context.Weights.Add(weightsActivity);
                _context.SaveChanges();

                MessageBox.Show("Weights activity added successfully!");
                Debug.WriteLine($"Added activity: Sets={sets}, Repetitions={repetitions}, WeightLifted={weightLifted}, Date={utcDate}, CaloriesBurned={caloriesBurned}");
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
                var activities = _context.Weights
                    .Where(w => w.UserId == userId)
                    .Select(w => new
                    {
                        w.Date,
                        w.Sets,
                        w.Repetitions,
                        w.WeightLifted,
                        w.CaloriesBurned
                    })
                    .ToList();

                WeightsDataGrid.ItemsSource = activities;

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
            SetsInput.Clear();
            RepetitionsInput.Clear();
            WeightLiftedInput.Clear();
            DateInput.SelectedDate = null;
        }

        private bool ValidateInputs(out int sets, out int repetitions, out double weightLifted, out DateTime activityDate)
        {
            sets = 0;
            repetitions = 0;
            weightLifted = 0;
            activityDate = DateTime.MinValue;

            return int.TryParse(SetsInput.Text, out sets) &&
                   int.TryParse(RepetitionsInput.Text, out repetitions) &&
                   double.TryParse(WeightLiftedInput.Text, out weightLifted) &&
                   DateInput.SelectedDate.HasValue &&
                   (activityDate = DateInput.SelectedDate.Value) > DateTime.MinValue;
        }
    }
}
