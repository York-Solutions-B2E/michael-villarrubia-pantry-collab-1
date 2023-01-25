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
        
        public async Task<Family> CreateFamily(FamilyDTO familyRequest, int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            
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
            };

            _context.Families.Add(family);
            await _context.SaveChangesAsync();
            await pantryService.CreatePantry(family.Id);
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
                throw new Exception("Invalid family code");
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

        public async Task<List<string>> GetFamiliesIngredients(int familyId)
        {
            var family = await _context.Families
                .Where(f => f.Id == familyId)
                .Include(f => f.Recipes)
                .ThenInclude(r => r.Ingredients)
                .FirstOrDefaultAsync();
            
            if(family == null)
            {
                throw new Exception("family not found");
            }
            
            var ingredients = new HashSet<string>();

            family.Recipes.ForEach(r =>
            {
                r.Ingredients.ForEach(i =>
                {
                    ingredients.Add(i.Name);
                });
            });

            return ingredients.ToList();

        }
    }
}
