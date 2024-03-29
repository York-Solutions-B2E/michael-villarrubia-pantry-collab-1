﻿using michael_villarrubia_pantry_collab_BE.DTOs;

namespace michael_villarrubia_pantry_collab_BE.Services.PantryService
{
    public interface IPantryService
    {
        Task CreatePantry(int familyId);
        Task<Pantry> AddItemToPantry(int familyId, PantryItemDTO item);
        Task<Pantry> GetPantry(int familyId);
        Task<Pantry> DeleteItem(int pantryId, int itemId);
        Task<Pantry> EditItem(int pantryId, int itemId, PantryItemDTO itemRequest);

    }
}
