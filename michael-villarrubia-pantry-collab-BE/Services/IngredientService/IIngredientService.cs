﻿using michael_villarrubia_pantry_collab_BE.DTOs;

namespace michael_villarrubia_pantry_collab_BE.Services.IngredientService
{
    public interface IIngredientService
    {
        Task<Ingredient> AddIngredient(int quantity, IngredientDTO ingredientRequest, int recipeId);
    }
}