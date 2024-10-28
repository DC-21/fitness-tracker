using System.ComponentModel.DataAnnotations;

namespace Fit.Models {
public class Walking : BaseActivity
{
    [Required]
    public int Steps { get; set; }
    [Required]
    public double Distance { get; set; }

    public double TimeTaken { get; set; }

    public DateTime Date { get; set; }
    }
}