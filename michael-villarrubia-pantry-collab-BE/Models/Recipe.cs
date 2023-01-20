using System.Text.Json.Serialization;

namespace michael_villarrubia_pantry_collab_BE.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        public int FamilyId { get; set; }
        public Family Family { get; set; }
        [JsonIgnore]
        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public List<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

    }
}
