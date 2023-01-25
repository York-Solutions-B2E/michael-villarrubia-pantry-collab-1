using michael_villarrubia_pantry_collab_BE.DTOs;

namespace michael_villarrubia_pantry_collab_BE.Services.FamilyService
{
    public interface IFamilyService
    {
        Task<Family> CreateFamily(FamilyDTO family, int userId);

        Task<Family> JoinFamily(string code, int userId);
        Task DeleteFamily(int familyId);
        Task<Family> GetFamily(int familyId);
        Task<List<string>> GetFamiliesIngredients(int familyId);
    }
}
