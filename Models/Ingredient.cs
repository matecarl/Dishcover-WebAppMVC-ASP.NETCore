using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dishcover.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Required]
        public ICollection<IngredientMeasurementUnit> SupportedUnits { get; set; }
    }
}
