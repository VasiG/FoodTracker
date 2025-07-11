using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodTracker.Data
{
    public enum MealType
    {
        Breakfast,
        Lunch,
        Dinner,
        Snack
    }
    public class Measure
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Date")]
        public DateTime MeasureDate { get; set; }
        [Required]
        [DisplayName("Blood sugar (before)")]
        public double SugarLevelBefore { get; set; }

        [DisplayName("Blood sugar (after)")]
        public double SugarLevelAfter { get; set; } = 0;
        [Required]
        [DisplayName("Type of meal")]
        public string MealType { get; set; }
        [MaxLength(150)]
        [DisplayName("Notes")]
        public string Notes { get; set; } = string.Empty;
    }
}
