using System.ComponentModel.DataAnnotations;

namespace Fit.Models
{
    public class Hiking : BaseActivity
    {
        [Required]
        public double Distance { get; set; }

        [Required]
        public double TimeTaken { get; set; }

        [Required]
        public double ElevationGain { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
