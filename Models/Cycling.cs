using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fit.Models
{

    public class Cycling : BaseActivity
    {
        [Required]
        public double Distance { get; set; }

        [Required]
        public double TimeTaken { get; set; }

        [Required]
        public double AverageSpeed { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
