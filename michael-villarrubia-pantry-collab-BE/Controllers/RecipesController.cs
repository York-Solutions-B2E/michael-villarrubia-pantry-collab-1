using michael_villarrubia_pantry_collab_BE.DTOs;
using michael_villarrubia_pantry_collab_BE.Services.RecipeService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace michael_villarrubia_pantry_collab_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        [HttpGet("getRecipes")]
        public async Task<ActionResult<List<Recipe>>> GetFamilyRecipes(int familyId)
        {
            try
            {
                return Ok(await recipeService.GetFamilyRecipes(familyId));
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult<Recipe>> CreateRecipe(RecipeDTO recipeRequest, int familyId)
        {
            try
            {
                return Ok(await recipeService.CreateRecipe(recipeRequest, familyId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
