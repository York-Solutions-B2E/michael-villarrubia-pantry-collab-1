namespace michael_villarrubia_pantry_collab_BE.Services.InvitationService
{
    public interface IInvitationService
    {
        Task<Invitation> SendInvitation(int senderFamilyId, string recieverFamilyCode);
        Task<Invitation> RespondToInvitation(int invitationId, bool reponse);
        Task<List<Invitation>> GetInvitations(int familyId);
    }
}
