namespace Dishcover.Models
{
    public class IngredientMeasurementUnit
    {
        public int Id { get; set; }
        public MeasurementUnit Unit { get; set; }
    }

    public enum MeasurementUnit
    {
        Amount,
        Grams,
        Millilitres,
        Pinches,
        Teaspoons,
        Tablespoons
    }
}
