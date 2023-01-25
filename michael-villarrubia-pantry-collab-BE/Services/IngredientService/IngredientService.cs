using michael_villarrubia_pantry_collab_BE.DTOs;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection.Metadata.Ecma335;

namespace michael_villarrubia_pantry_collab_BE.Services.IngredientService
{
    public class IngredientService : IIngredientService
    {
        private readonly DataContext _context;

        public IngredientService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Ingredient>> AddIngredient(int quantity, List<IngredientDTO> ingredientRequest, int recipeId)
        {
            var ingredients = await _context.Ingredients
                .Include(i => i.Recipes)
                .AsSplitQuery()
                .Include(i => i.RecipeIngredients)
                .AsSplitQuery()
                .ToListAsync();
            
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
            
            if (ingredientRequest.Count > 0)
            {
                foreach(var i in ingredientRequest)  
                {
                    var existIngredient = ingredients.FirstOrDefault(existing => existing.Name == i.Name);
                    if (existIngredient != null)
                    {
                        var _recipeIngredient = new RecipeIngredient
                        {
                            Ingredient = existIngredient,
                            Recipe = recipe,
                            Quantity = i.recipeIngredients[0].Quantity,
                        };

                        recipe.RecipeIngredients.Add(_recipeIngredient);
                        existIngredient.RecipeIngredients.Add(_recipeIngredient);

                        recipe.Ingredients.Add(existIngredient);
                        existIngredient.Recipes.Add(recipe);
                        continue;
                    }
                    var newIngredient = new Ingredient
                    {
                        Name = i.Name,
                        UnitOfMeasurement = i.UnitOfMeasurement,
                    };

                    _context.Ingredients.Add(newIngredient);

                    var recipeIngredient = new RecipeIngredient
                    {
                        Ingredient = newIngredient,
                        Recipe = recipe,
                        Quantity = i.recipeIngredients[0].Quantity,
                    };

                    recipe.RecipeIngredients.Add(recipeIngredient);
                    newIngredient.RecipeIngredients.Add(recipeIngredient);

                    recipe.Ingredients.Add(newIngredient);
                    newIngredient.Recipes.Add(recipe);
                };
            }
            await _context.SaveChangesAsync();

            return ingredients;
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
                var recipe = family.Recipes.Find(r => r.Name == itemName);
                
                if (recipe != null)
                {
                    var ingredients = recipe.Ingredients;
                    return ingredients;
                }

                return new List<Ingredient>();
            }

            throw new Exception("Family not found");
        }
    }
}
