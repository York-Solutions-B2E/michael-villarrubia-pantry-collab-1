using michael_villarrubia_pantry_collab_BE.DTOs;
using michael_villarrubia_pantry_collab_BE.Services.IngredientService;
using Microsoft.EntityFrameworkCore;

namespace michael_villarrubia_pantry_collab_BE.Services.RecipeService
{
    public class RecipeService : IRecipeService
    {
        private readonly DataContext _context;
        private readonly IIngredientService ingredientService;

        public RecipeService(DataContext context, IIngredientService ingredientService)
        {
            _context = context;
            this.ingredientService = ingredientService;
        }

        public async Task<Recipe> CreateRecipe(RecipeDTO recipeRequest, int familyId)
        {
            var family = await _context.Families
                .Include(f => f.SentInvitations)
                .ThenInclude(i => i.ReceiverFamily)
                .Include(f => f.ReceivedInvitations)
                .ThenInclude(i => i.SenderFamily)
                .Include(f => f.Recipes)
                .FirstOrDefaultAsync(f => f.Id == familyId);
            
            if (family == null)
            {
                throw new Exception("You must join a family first.");
            }
            
            var newRecipe = await _context.Recipes
                .FirstOrDefaultAsync(r => r.Name == recipeRequest.Name && r.Creator == family.Name);

            if (newRecipe == null)
            {
                newRecipe = new Recipe
                {
                    Name = recipeRequest.Name,
                    Creator = family.Name,
                    Image = recipeRequest.Image,
                    Instructions = recipeRequest.Instructions,
                };
                _context.Recipes.Add(newRecipe);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Your family already has a recipe for " + recipeRequest.Name);
            }

            await ingredientService.AddIngredient(1, recipeRequest.Ingredients, newRecipe.Id);

            var familiesToAdd = new List<Family>();
            family.SentInvitations.Where(i => i.Accepted == true)
                .ToList()
                .ForEach(i =>
                {
                    familiesToAdd.Add(i.ReceiverFamily);
                });

            family.ReceivedInvitations.Where(i => i.Accepted == true)
                .ToList()
                .ForEach(i =>
                {
                    familiesToAdd.Add(i.SenderFamily);
                });


            family.Recipes.Add(newRecipe);
            familiesToAdd.ForEach(f =>
            {
                f.Recipes.Add(newRecipe);
            });
            newRecipe.FamiliesWithAccess = familiesToAdd;
            newRecipe.FamiliesWithAccess.Add(family);

            await _context.SaveChangesAsync();

            return newRecipe;
        }

        public async Task<List<Recipe>> GetFamilyRecipes(int familyId)
        {
            var family = await _context.Families
                .Include(f => f.Recipes)
                .ThenInclude(r => r.Ingredients)
                .ThenInclude(i => i.RecipeIngredients)
                .FirstOrDefaultAsync(f => f.Id == familyId);

            if (family != null)
            {
                var recipes = family.Recipes;
                return recipes;
            }

            throw new Exception("family not found");
        }

        public async Task<List<Recipe>> DeleteRecipe(int familyId, int recipeId)
        {
            var family = await _context.Families.FindAsync(familyId);
            var recipe = await _context.Recipes.FindAsync(recipeId);

            if(family == null)
            {
                throw new Exception("family not found");
            }
            if(recipe == null) 
            {
                throw new Exception("recipe not found");
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return await GetFamilyRecipes(familyId);
        }

        public async Task<List<Recipe>> EditRecipe(int recipeId, RecipeDTO recipeRequest, int familyId)
        {
            var recipe = await _context.Recipes
                .Include(r => r.Ingredients)
                .ThenInclude(i => i.RecipeIngredients)
                .FirstOrDefaultAsync(r => r.Id == recipeId);

            if(recipe == null)
            {
                throw new Exception("recipe not found");
            }


            await ingredientService.ChangeIngredients(recipe, recipeRequest.Ingredients);
            recipe.Instructions = recipeRequest.Instructions;
            recipe.Image = recipeRequest.Image;
            recipe.Name = recipeRequest.Name;

            await _context.SaveChangesAsync();

            return await GetFamilyRecipes(familyId);
        } 
    }
}
