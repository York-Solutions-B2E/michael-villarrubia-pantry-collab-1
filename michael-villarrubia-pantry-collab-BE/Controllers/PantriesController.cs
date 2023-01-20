﻿using michael_villarrubia_pantry_collab_BE.DTOs;
using michael_villarrubia_pantry_collab_BE.Services.PantryService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace michael_villarrubia_pantry_collab_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PantriesController : ControllerBase
    {
        private readonly IPantryService _pantryService;

        public PantriesController(IPantryService pantryService)
        {
            _pantryService = pantryService;
        }

        [HttpGet]
        public async Task<ActionResult<Pantry>> GetPantry(int familyId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("addItem")]
        public async Task<ActionResult<Pantry>> AddItemToPantry(int familyId, PantryItemDTO item)
        {
            try
            {
                return Ok(await _pantryService.AddItemToPantry(familyId, item));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}