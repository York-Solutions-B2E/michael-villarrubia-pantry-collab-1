namespace michael_villarrubia_pantry_collab_BE.DTOs
{
    public class RecipeDTO
    { 
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Instructions { get; set; } = string.Empty;
        public List<IngredientDTO> Ingredients { get; set; } = new List<IngredientDTO>();
    }
}
