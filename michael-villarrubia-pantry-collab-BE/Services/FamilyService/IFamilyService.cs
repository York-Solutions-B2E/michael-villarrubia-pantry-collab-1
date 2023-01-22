using michael_villarrubia_pantry_collab_BE.DTOs;

namespace michael_villarrubia_pantry_collab_BE.Services.FamilyService
{
    public interface IFamilyService
    {
        Task<Family> CreateFamily(FamilyDTO family, string userCreatingFamily);

        Task<Family> JoinFamily(string code, string password, int userId);
        Task DeleteFamily(int familyId);
        Task<Family> GetFamily(int familyId);
    }
}
