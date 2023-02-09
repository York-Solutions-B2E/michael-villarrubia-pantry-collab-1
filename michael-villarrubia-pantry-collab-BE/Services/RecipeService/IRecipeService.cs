using michael_villarrubia_pantry_collab_BE.DTOs;

namespace michael_villarrubia_pantry_collab_BE.Services.RecipeService
{
    public interface IRecipeService
    {
        Task<Recipe> CreateRecipe(RecipeDTO recipeRequest, int familyId);
        Task<List<Recipe>> GetFamilyRecipes(int familyId);
        Task<List<Recipe>> DeleteRecipe(int familyId, int recipeId);
        Task<List<Recipe>> EditRecipe(int recipeId, RecipeDTO recipeRequest, int familyId);
    }
}
