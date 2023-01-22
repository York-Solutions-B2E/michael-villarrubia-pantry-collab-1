using System.Text.Json.Serialization;

namespace michael_villarrubia_pantry_collab_BE.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string UnitOfMeasurement { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
        public List<RecipeIngredient> RecipeIngredients { get; set;} = new List<RecipeIngredient>();
    }
}
