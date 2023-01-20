using michael_villarrubia_pantry_collab_BE.DTOs;
using Microsoft.EntityFrameworkCore;

namespace michael_villarrubia_pantry_collab_BE.Services.RecipeService
{
    public class RecipeService : IRecipeService
    {
        private readonly DataContext _context;

        public RecipeService(DataContext context)
        {
            _context = context;
        }

        public async Task<Recipe> CreateRecipe(RecipeDTO recipeRequest, int familyId)
        {
            var newRecipe = await _context.Recipes
                .FirstOrDefaultAsync(r => r.Name == recipeRequest.Name && r.FamilyId == familyId);

            var family = await _context.Families
                .Include(f => f.Recipes)
                .FirstOrDefaultAsync(f => f.Id == familyId);

            if (family == null)
            {
                throw new Exception("You must join a family first.");
            }

            if (newRecipe == null)
            {
                newRecipe = new Recipe
                {
                    Name = recipeRequest.Name,
                    FamilyId = familyId,
                    Image = recipeRequest.Image,
                    Family = family,
                };

                _context.Recipes.Add(newRecipe);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Your family already has a recipe for " + recipeRequest.Name);
            }

            family.Recipes.Add(newRecipe);
            await _context.SaveChangesAsync();

            return newRecipe;
        }

        public async Task<List<Recipe>> GetFamilyRecipes(int familyId)
        {
            var family = await _context.Families
                .Include(f => f.Recipes)
                .ThenInclude(r => r.RecipeIngredients)
                .FirstOrDefaultAsync(f => f.Id == familyId);

            if (family != null)
            {
                var recipes = family.Recipes.ToList();
                return recipes;
            }

            throw new Exception("");
        }
    }
}
