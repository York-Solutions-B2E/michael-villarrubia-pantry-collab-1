using michael_villarrubia_pantry_collab_BE.DTOs;
using Microsoft.EntityFrameworkCore.Query;

namespace michael_villarrubia_pantry_collab_BE.Services.PantryService
{
    public class PantryService : IPantryService
    {
        private readonly DataContext _context;

        public PantryService(DataContext context)
        {
            _context = context;
        }

        public async Task CreatePantry(int familyId)
        {
            var family = await _context.Families.Include(x => x.Pantry).FirstOrDefaultAsync(x => x.Id == familyId);

            if (family != null)
            {
                family.Pantry = new Pantry
                {
                    Family = family,
                    FamilyId = familyId,
                    Items = new List<PantryItem>()
                };
                _context.Pantries.Add(family.Pantry);
                await _context.SaveChangesAsync();
                return;
            }
            else
            {
                throw new Exception("Family doesn't exist");
            }
        }

        public async Task<Pantry> AddItemToPantry(int familyId, PantryItemDTO item)
        {
            var pantry = await _context.Pantries.Include(x => x.Items).Include(x => x.Family).FirstOrDefaultAsync(x => x.FamilyId == familyId);
            if (pantry != null)
            {
                var itemToAdd = new PantryItem
                {
                    PantryId = pantry.Id,
                    Pantry = pantry,
                    Calories = item.Calories,
                    Image = item.Image,
                    Name= item.Name,
                    QuantityInPantry= item.QuantityInPantry,
                    Weight = item.Weight,
                    UnitOfMeasurement= item.UnitOfMeasurement,
                };

                pantry.Items.Add(itemToAdd);
                _context.PantryItems.Add(itemToAdd);
                await _context.SaveChangesAsync();

                return pantry;
            }
            throw new Exception("Family does not have a pantry");
        }

        public async Task<Pantry> GetPantry(int familyId)
        {
            var pantry = await _context.Pantries.Include(x => x.Items).FirstOrDefaultAsync(x => x.FamilyId == familyId);
            if (pantry != null)
            {
                return pantry;
            }
            throw new Exception("Family does not have a pantry or does not exist");
        }

        public async Task<Pantry> DeleteItem(int pantryId, int itemId)
        {
            var pantry = await _context.Pantries.Include(p => p.Items).FirstOrDefaultAsync(p => p.Id == pantryId);
            var item = await _context.PantryItems.FindAsync(itemId);
            
            if(pantry == null)
            {
                throw new Exception("Pantry not found");
            }

            if (item == null)
            {
                throw new Exception("Item not found");
            }

            pantry.Items.Remove(item);
            _context.PantryItems.Remove(item);
            await _context.SaveChangesAsync();

            return pantry;
        }

        public async Task<Pantry> EditItem(int pantryId, int itemId, PantryItemDTO itemRequest)
        {
            var pantry = await _context.Pantries.Include(p => p.Items).FirstOrDefaultAsync(p => p.Id == pantryId);
            var item = await _context.PantryItems.FindAsync(itemId);

            if(pantry == null)
            {
                throw new Exception("Pantry not found");
            }

            if (item == null)
            {
                throw new Exception("Item not found");
            }
            item.UnitOfMeasurement= itemRequest.UnitOfMeasurement;
            item.QuantityInPantry = itemRequest.QuantityInPantry;
            item.Calories = itemRequest.Calories;
            item.Name = itemRequest.Name;
            item.Weight = itemRequest.Weight;
            item.Image= itemRequest.Image;

            await _context.SaveChangesAsync();

            return pantry;
        }
    }
}
