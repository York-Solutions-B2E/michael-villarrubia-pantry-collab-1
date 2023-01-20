using michael_villarrubia_pantry_collab_BE.DTOs;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace michael_villarrubia_pantry_collab_BE.Services.IngredientService
{
    public class IngredientService : IIngredientService
    {
        private readonly DataContext _context;

        public IngredientService(DataContext context)
        {
            _context = context;
        }

        public async Task<Ingredient> AddIngredient(int quantity, IngredientDTO ingredientRequest, int recipeId)
        {
            var ingredient = await _context.Ingredients
                .Include(i => i.Recipes)
                .Include(i => i.RecipeIngredients)
                .FirstOrDefaultAsync(i => i.Name == ingredientRequest.Name);
            
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.RecipeIngredients)
                .FirstOrDefaultAsync(r => r.Id == recipeId);
            
            if(recipe == null)
            {
                throw new Exception("You must create a recipe to add ingredients to it!");
            }
            
            if (ingredient == null)
            {
                ingredient = new Ingredient
                {
                    Name = ingredientRequest.Name,
                    UnitOfMeasurement = ingredientRequest.UnitOfMeasurement,
                };
                
                ingredient.Recipes.Add(recipe);
                _context.Ingredients.Add(ingredient);
                await _context.SaveChangesAsync();
            }

            recipe.Ingredients.Add(ingredient);
            
            await _context.SaveChangesAsync();
            ingredient = await ChangeQuantityAsync(ingredient.Id, recipe.Id, quantity);

            return ingredient;
        }

        public async Task<Ingredient> ChangeQuantityAsync(int ingredientId, int recipeId, int quantity)
        {
            var ingredient = await _context.Ingredients
                .Include(i => i.RecipeIngredients)
                .FirstOrDefaultAsync(i => i.Id == ingredientId);

            var recipe = await _context.Recipes
                .Include(r => r.RecipeIngredients)
                .FirstOrDefaultAsync(r => r.Id == recipeId);
            if (recipe != null && ingredient != null)
            {
                var recipeIngredient = ingredient.RecipeIngredients.Find
                    (ri => ri.IngredientId == ingredient.Id && ri.RecipeId == recipeId);

                if(recipeIngredient != null)
                {
                    recipeIngredient.Quantity = quantity;
                    await _context.SaveChangesAsync();
                    return ingredient;
                }

                throw new Exception("Ingredient wasn't found for this recipe");
            }

            throw new Exception("Recipe or ingredient not found");
        }
    }
}
