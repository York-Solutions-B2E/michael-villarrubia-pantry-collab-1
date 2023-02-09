using michael_villarrubia_pantry_collab_BE.DTOs;

namespace michael_villarrubia_pantry_collab_BE.Services.IngredientService
{
    public interface IIngredientService
    {
        Task<List<Ingredient>> AddIngredient(int quantity, List<IngredientDTO> ingredientRequest, int recipeId);
        Task<List<Ingredient>> GetUsedIngredients(int familyId, string itemName);
        Task<Ingredient> ChangeQuantityAsync(int ingredientId, int recipeId, int quantity);
        Task<List<Ingredient>> ChangeIngredients(Recipe recipe, List<IngredientDTO> ingredientsRequest);
        Task<Recipe> RemoveAllIngredients(int recipeId);


    }
}
