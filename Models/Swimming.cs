using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Fit.Models
{
    public class Swimming : BaseActivity
    {
        [Required]
        public int Laps { get; set; }
        [Required]
        public double TimeTaken { get; set; }
        [Required]
        public double AverageHeartRate { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}