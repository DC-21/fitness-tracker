using System.ComponentModel.DataAnnotations;

namespace Fit.Models
{
    public class Weights : BaseActivity
    {
        [Required]
        public int Sets { get; set; }

        [Required]
        public int Repetitions { get; set; }

        [Required]
        public double WeightLifted { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
