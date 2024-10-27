using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fit.Models;

namespace Fit.Models
{
    public abstract class BaseActivity
    {
        [Key]
        public int Id { get; set; }

        public double CaloriesBurned { get; set; } = 0;

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
