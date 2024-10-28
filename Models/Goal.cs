using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fit.Models
{
    public class Goal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public double CalorieTarget { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        public double TotalCaloriesBurned { get; set; } = 0;

        public bool IsAchieved { get; set; } = false;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }


        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public int? WalkingId { get; set; }
        public Walking Walking { get; set; }

        public int? CyclingId { get; set; }
        public Cycling Cycling { get; set; }

        public int? RunningId { get; set; }
        public Running Running { get; set; }

        public int? SwimmingId { get; set; }
        public Swimming Swimming { get; set; }

        public int? WeightsId { get; set; }
        public Weights Weights { get; set; }

        public int? HikingId { get; set; }
        public Hiking Hiking { get; set; }
    }
}
