﻿using michael_villarrubia_pantry_collab_BE.DTOs;
using michael_villarrubia_pantry_collab_BE.Services.PantryService;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace michael_villarrubia_pantry_collab_BE.Controllers
{
    [EnableCors("Angular")]
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
            try
            {
                return Ok(await _pantryService.GetPantry(familyId));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
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

        [HttpDelete("deleteItem")]
        public async Task<ActionResult<Pantry>> DeleteItem(int pantryId, int itemId)
        {
            try
            {
                return Ok(await _pantryService.DeleteItem(pantryId, itemId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("editItem")]
        public async Task<ActionResult<Pantry>> EditItem(int pantryId, int itemId, PantryItemDTO itemRequest)
        {
            try
            {
                return Ok(await _pantryService.EditItem(pantryId, itemId, itemRequest));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
