using ERPSystem.Models.Manufacturing;
using ERPSystem.Services.Manufacturing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ERPSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(IInventoryService inventoryService, ILogger<InventoryController> logger)
        {
            _inventoryService = inventoryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> GetAll()
        {
            try
            {
                var inventories = await _inventoryService.GetAllItemsAsync();
                return Ok(inventories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all inventory items.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetById(int id)
        {
            try
            {
                var inventory = await _inventoryService.GetItemByIdAsync(id);
                if (inventory == null)
                {
                    return NotFound();
                }
                return Ok(inventory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while getting inventory item with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Inventory inventory)
        {
            if (inventory == null)
            {
                return BadRequest("Inventory cannot be null.");
            }

            try
            {
                await _inventoryService.AddItemAsync(inventory);
                return CreatedAtAction(nameof(GetById), new { id = inventory.Id }, inventory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new inventory item.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return BadRequest("ID mismatch.");
            }

            try
            {
                await _inventoryService.UpdateItemAsync(inventory);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while updating inventory item with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _inventoryService.DeleteItemAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting inventory item with ID {id}.");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("find")]
        public async Task<ActionResult<Inventory>> Find([FromQuery] string name)
        {
            try
            {
                var inventory = await _inventoryService.FindItemAsync(i => i.Product != null && i.Product.ProductName == name);
                if (inventory == null)
                {
                    return NotFound();
                }
                return Ok(inventory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while finding inventory item with product name {name}.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}