using michael_villarrubia_pantry_collab_BE.DTOs;
using michael_villarrubia_pantry_collab_BE.Services.PantryService;

namespace michael_villarrubia_pantry_collab_BE.Services.FamilyService
{
    public class FamilyService : IFamilyService
    {
        private readonly DataContext _context;
        private readonly IPantryService pantryService;

        public FamilyService(DataContext context, IPantryService pantryService)
        {
            _context = context;
            this.pantryService = pantryService;
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
                Code = user.Id.ToString("D7"),
                Password = familyRequest.Password
            };

            _context.Families.Add(family);
            await _context.SaveChangesAsync();
            await pantryService.CreatePantry(family.Id);
            await JoinFamily(family.Code, family.Password, user.Id);

            return family;
        }

        public async Task<Family> JoinFamily(string code, string password, int userId)
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
                throw new Exception("Invalid family code");
            }

            if(family.Password != password)
            {
                throw new Exception("Invalid password");
            }

            user.FamilyId = family.Id;
            family.Users.Add(user);
            await _context.SaveChangesAsync();

            return family;
        }

        public async Task DeleteFamily (int familyId)
        {
            var familyToDelete = await _context.Families.Include(x => x.Users).FirstOrDefaultAsync(x => x.Id == familyId);
            
            if(familyToDelete != null)
            {
                familyToDelete.Users.Clear();
                _context.Families.Remove(familyToDelete);
                await _context.SaveChangesAsync();
                return;
            }
            throw new Exception("Family doesn't exist");
        }

        public async Task<Family> GetFamily(int familyId)
        {
            var family = await _context.Families
                .FirstOrDefaultAsync(f => f.Id == familyId); 

            if(family == null)
            {
                throw new Exception("Family not found");
            }
            return family;
        }
    }
}
