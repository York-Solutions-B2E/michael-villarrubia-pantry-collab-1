using System.Text.Json.Serialization;

namespace michael_villarrubia_pantry_collab_BE.Models
{
    public class RecipeIngredient
    {
        public int Quantity { get; set; }
        public int RecipeId { get; set; }
        [JsonIgnore]
        public Recipe Recipe { get; set; }
        public int IngredientId { get; set; }
        [JsonIgnore]
        public Ingredient Ingredient { get; set; }
    }
}
