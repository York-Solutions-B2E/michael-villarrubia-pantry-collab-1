using michael_villarrubia_pantry_collab_BE.DTOs;

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
                .FirstOrDefaultAsync(i => i.Name == ingredientRequest.Name);
            
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
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

            return ingredient;
        }
    }
}
