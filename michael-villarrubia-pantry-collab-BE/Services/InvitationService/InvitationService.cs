using Microsoft.AspNetCore.Mvc;

namespace michael_villarrubia_pantry_collab_BE.Services.InvitationService
{
    public class InvitationService : IInvitationService
    {
        private readonly DataContext _context;

        public InvitationService(DataContext context)
        {
            _context = context;
        }
        
        public async Task<Invitation> SendInvitation(int senderFamilyId, string recieverFamilyCode)
        {
            var sender = await _context.Families
                .Include(f => f.SentInvitations)
                .FirstOrDefaultAsync(f => f.Id == senderFamilyId);

            var receiver = await _context.Families
                .Include(f => f.ReceivedInvitations)
                .FirstOrDefaultAsync(f => f.Code == recieverFamilyCode);

            if(sender == null)
            {
                throw new Exception("Your family not found (are you logged in a member of a family?)");
            }

            if(receiver == null)
            {
                throw new Exception("Receiving family not found");
            }

            var invitation = new Invitation
            {
                SenderFamily = sender,
                ReceiverFamily = receiver,
            };

            _context.Invitations.Add(invitation);

            await _context.SaveChangesAsync();

            sender.SentInvitations.Add(invitation);
            receiver.ReceivedInvitations.Add(invitation);

            await _context.SaveChangesAsync();

            return invitation;
        }

        public async Task<Invitation> RespondToInvitation(int invitationId, bool response)
        {
            var invitation = await _context.Invitations.FindAsync(invitationId);

            if (invitation == null)
            {
                throw new Exception("Invitation not found");
            }

            var responderFam = await _context.Families
                .Include(f => f.ReceivedInvitations)
                .Include(f => f.Recipes)
                .FirstOrDefaultAsync(f => f.Id == invitation.ReceiverFamilyId);

            var senderFam = await _context.Families
                .Include(f => f.SentInvitations)
                .Include (f => f.Recipes)
                .FirstOrDefaultAsync(f => f.Id == invitation.SenderFamilyId);
            
            if (responderFam == null)
            {
                throw new Exception("Your family could not be found");
            }

            if (senderFam == null)
            {
                throw new Exception("Whichever family sent that invitation does't exist anymore...");
            }

            invitation.Accepted = response;
            
            if (response)
            {
                responderFam.Recipes.AddRange(senderFam.Recipes.Where(r => r.Creator == senderFam.Name));
                senderFam.Recipes.AddRange(responderFam.Recipes.Where(r => r.Creator == responderFam.Name));
            }

            await _context.SaveChangesAsync();
            return invitation;
        }

        public async Task<List<Invitation>> GetInvitations(int familyId)
        {
            var invitations = await _context.Invitations.
                Where(i => i.SenderFamilyId == familyId || i.ReceiverFamilyId == familyId)
                .Include(i => i.SenderFamily)
                .Include(i => i.ReceiverFamily)
                .ToListAsync();
            if (invitations == null)
            {
                return new List<Invitation>();
            }

            return invitations; 
        }
    }
}
