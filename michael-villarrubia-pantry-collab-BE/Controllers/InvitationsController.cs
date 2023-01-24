using michael_villarrubia_pantry_collab_BE.Services.InvitationService;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace michael_villarrubia_pantry_collab_BE.Controllers
{
    [EnableCors("Angular")]
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationsController : ControllerBase
    {
        private readonly IInvitationService invitationService;

        public InvitationsController(IInvitationService invitationService)
        {
            this.invitationService = invitationService;
        }

        [HttpPost("send")]
        public async Task<ActionResult<Invitation>> SendInvitation(int senderFamId, string receiverFamCode)
        {
            try
            {
                return Ok(await invitationService.SendInvitation(senderFamId, receiverFamCode));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("respond")]
        public async Task<ActionResult<Invitation>> RespondToInvitation(int invitationId, bool response)
        {
            try
            {
                return Ok(await invitationService.RespondToInvitation(invitationId, response));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Invitation>>> GetInvitations(int familyId)
        {
            return Ok(await invitationService.GetInvitations(familyId));
        }
    }
}
