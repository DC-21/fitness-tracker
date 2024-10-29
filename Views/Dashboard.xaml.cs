using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Fit.Data;

namespace Fit.Views
{
    public partial class Dashboard : Page
    {
        private readonly FitDbContext _context;

        public int GoalCount { get; set; }

        public Dashboard()
        {
            InitializeComponent();
            _context = new FitDbContext();
            DataContext = this;
            LoadUserGoals(); // Load goals when the page is initialized
        }

        private void LoadUserGoals()
        {
            if (!TryGetUserId(out int userId))
                return;

            // Load user goals as you already have
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
            GoalCount = userGoals.Count;

            // Calculate and display total calories burned
            int totalCalories = CalculateTotalCaloriesBurned(userId);
            CaloriesBurnedTextBlock.Text = $"Total Calories Burned: {totalCalories}";

            if (userGoals.Any())
            {
                Debug.WriteLine($"Loaded {GoalCount} goals for user ID {userId}.");
            }
            else
            {
                MessageBox.Show("No goals found for this user.");
                Debug.WriteLine($"No goals found for user ID {userId}.");
            }
        }

        private int CalculateTotalCaloriesBurned(int userId)
        {
            // Initialize the total calories burned
            int totalCalories = 0;

            // Add calories burned from Cyclings
            totalCalories += _context.Cyclings
                .Where(c => c.UserId == userId)
                .Sum(c => (int)c.CaloriesBurned);

            // Add calories burned from Hikings
            totalCalories += _context.Hikings
                .Where(h => h.UserId == userId)
                .Sum(h => (int)h.CaloriesBurned);

            // Add calories burned from Runnings
            totalCalories += _context.Runnings
                .Where(r => r.UserId == userId)
                .Sum(r => (int)r.CaloriesBurned);

            // Add calories burned from Swimmings
            totalCalories += _context.Swimmings
                .Where(s => s.UserId == userId)
                .Sum(s => (int)s.CaloriesBurned);

            // Add calories burned from Walkings
            totalCalories += _context.Walkings
                .Where(w => w.UserId == userId)
                .Sum(w => (int)w.CaloriesBurned);

            // Add calories burned from Weights
            totalCalories += _context.Weights
                .Where(wt => wt.UserId == userId)
                .Sum(wt => (int)wt.CaloriesBurned);

            return totalCalories;
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
    }
}
