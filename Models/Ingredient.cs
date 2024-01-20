using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dishcover.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Display(Name = "DeletedAt")]
        public DateTime? DeletedAt { get; set; }

        [Required]
        public ICollection<IngredientMeasurementUnit> SupportedUnits { get; set; } = new List<IngredientMeasurementUnit>();

    }
}
