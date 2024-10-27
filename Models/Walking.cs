using System.ComponentModel.DataAnnotations;

namespace Fit.Models {
public class Walking : BaseActivity
{
    [Required]
    public int Steps { get; set; }
    [Required]
    public double Distance { get; set; }
    [Required]
    public double TimeTaken { get; set; }
}
}