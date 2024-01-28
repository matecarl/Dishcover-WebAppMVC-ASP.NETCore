using Dishcover.Resources;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dishcover.Models
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        [Display(Name = "DeletedAt")]
        public DateTime? DeletedAt { get; set; }

        [Required(ErrorMessageResourceType = typeof(SharedResources), ErrorMessageResourceName = "Required")]
        public ICollection<IngredientMeasurementUnit> SupportedUnits { get; set; } = new List<IngredientMeasurementUnit>();

    }
}
