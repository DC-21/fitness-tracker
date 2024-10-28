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

            GoalCount = userGoals.Count; // Set GoalCount to the number of goals

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
