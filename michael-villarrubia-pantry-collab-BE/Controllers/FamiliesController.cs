using michael_villarrubia_pantry_collab_BE.DTOs;
using michael_villarrubia_pantry_collab_BE.Services.FamilyService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace michael_villarrubia_pantry_collab_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamiliesController : ControllerBase
    {
        private readonly IFamilyService _familyService;

        public FamiliesController(IFamilyService familyService)
        {
            _familyService = familyService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Family>> CreateFamily(FamilyDTO familyRequest, string userCreatingFamily)
        {
            try
            {
                return Ok(await _familyService.CreateFamily(familyRequest, userCreatingFamily));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("join")]
        public async Task<ActionResult<Family>> JoinFamily(string code, string password, int userId)
        {
            try
            {
                return Ok(await _familyService.JoinFamily(code, password, userId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteFamily(int familyId)
        {
            try
            {
                await _familyService.DeleteFamily(familyId);
                return Ok();
            }
            catch (Exception e )
            {
                return BadRequest(e.Message);
            }
        }
    }
}
