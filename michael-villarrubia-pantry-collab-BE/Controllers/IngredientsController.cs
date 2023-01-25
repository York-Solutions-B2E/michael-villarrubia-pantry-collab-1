using michael_villarrubia_pantry_collab_BE.DTOs;
using michael_villarrubia_pantry_collab_BE.Services.IngredientService;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace michael_villarrubia_pantry_collab_BE.Controllers
{
    [EnableCors("Angular")]
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            this.ingredientService = ingredientService;
        }

        [HttpPost("add")]
        public async Task<ActionResult<List<Ingredient>>> AddIngredient(
            int quantity,
            List<IngredientDTO> ingredientRequest,
            int recipeId)
        {
            try
            {
                var ingredient = await ingredientService.AddIngredient(quantity, ingredientRequest, recipeId);
                return Ok(ingredient);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("showIngredients")]
        public async Task<ActionResult<List<Ingredient>>> GetUsedIngredients(int familyId, string itemName)
        {
            try
            {
                return Ok(await ingredientService.GetUsedIngredients(familyId, itemName));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
