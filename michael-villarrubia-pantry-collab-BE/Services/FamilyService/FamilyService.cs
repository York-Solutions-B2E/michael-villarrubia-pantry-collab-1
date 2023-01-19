using michael_villarrubia_pantry_collab_BE.DTOs;

namespace michael_villarrubia_pantry_collab_BE.Services.FamilyService
{
    public class FamilyService : IFamilyService
    {
        private readonly DataContext _context;

        public FamilyService(DataContext context)
        {
            _context = context;
        }
        
        public async Task<Family> CreateFamily(FamilyDTO familyRequest, string userCreatingFamily)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == userCreatingFamily);
            
            if(user == null)
            {
                throw new Exception("You must login to create a family!");
            }

            if(user.FamilyId != null)
            {
                throw new Exception("You already belong to a family");
            }

            var family = new Family
            {
                Name = familyRequest.Name,
                Users = new List<User>(),
                Code = new Random().Next(0, 999999).ToString("D6")
            };

            _context.Families.Add(family);
            await _context.SaveChangesAsync();
            await JoinFamily(family.Code, user.Id);

            return family;
        }

        public async Task<Family> JoinFamily(string code, int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            var family = await _context.Families.Include(x => x.Users).FirstOrDefaultAsync(x => x.Code == code);

            if(user == null)
            {
                throw new Exception("You must login to join a family.");
            }

            if(user.FamilyId != null)
            {
                throw new Exception("You are already a member of a family.");
            }

            if(family == null)
            {
                throw new Exception("Invalid family code.");
            }

            user.FamilyId = family.Id;
            family.Users.Add(user);
            await _context.SaveChangesAsync();

            return family;
        }
    }
}
