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
                .AsSplitQuery()
                .Include(i => i.RecipeIngredients)
                .AsSplitQuery()
                .FirstOrDefaultAsync(i => i.Name == ingredientRequest.Name);
            
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .AsSplitQuery()
                .Include(r => r.RecipeIngredients)
                .AsSplitQuery()
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
                
                _context.Ingredients.Add(ingredient);
            }

            var recipeIngredient = new RecipeIngredient
            {
                Ingredient = ingredient,
                Recipe = recipe,
                Quantity = quantity,
            };

            recipe.RecipeIngredients.Add(recipeIngredient);
            ingredient.RecipeIngredients.Add(recipeIngredient);

            recipe.Ingredients.Add(ingredient);
            ingredient.Recipes.Add(recipe);

            await _context.SaveChangesAsync();

            return ingredient;
        }

        public async Task<Ingredient> ChangeQuantityAsync(int ingredientId, int recipeId, int quantity)
        {
            
            var ingredientTask = _context.Ingredients
                .Include(i => i.Recipes)
                .Include(i => i.RecipeIngredients)
                .FirstOrDefaultAsync(i => i.Id == ingredientId);

            await ingredientTask;
            
            var recipeTask = _context.Recipes
                .Include(r => r.Ingredients)
                .Include(r => r.RecipeIngredients)
                .FirstOrDefaultAsync(r => r.Id == recipeId);

            await recipeTask;

            var ingredient = ingredientTask.Result;
            var recipe = recipeTask.Result;

            if (recipe != null && ingredient != null)
            {
                var recipeIngredient = ingredient.RecipeIngredients.Find
                    (ri => ri.IngredientId == ingredient.Id && ri.RecipeId == recipe.Id);

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

        public async Task<List<Ingredient>> GetUsedIngredients(int familyId, string itemName)
        {
            var family = await _context.Families
                .Include(f => f.Pantry)
                .ThenInclude(p => p.Items)
                .Include(f => f.Recipes)
                .ThenInclude(r => r.Ingredients)
                .FirstOrDefaultAsync(f => f.Id == familyId);

            if(family != null)
            {
                var recipe = family.Recipes.ToList().Find(r => r.Name == itemName);
                
                if (recipe != null)
                {
                    var ingredients = recipe.Ingredients.ToList();
                    return ingredients;
                }

                return new List<Ingredient>();
            }

            throw new Exception("Family not found");
        }
    }
}
