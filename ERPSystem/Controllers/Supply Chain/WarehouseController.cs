using ERPSystem.Models.Supply_Chain;
using ERPSystem.Services.SupplyChain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Controllers.SupplyChain
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetAllWarehouses()
        {
            var warehouses = await _warehouseService.GetAllWarehousesAsync();
            return Ok(warehouses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Warehouse?>> GetWarehouseById(int id)
        {
            var warehouse = await _warehouseService.GetWarehouseByIdAsync(id);
            if (warehouse == null)
            {
                return NotFound();
            }
            return Ok(warehouse);
        }

        [HttpPost]
        public async Task<ActionResult> CreateWarehouse(Warehouse warehouse)
        {
            await _warehouseService.CreateWarehouseAsync(warehouse);
            return CreatedAtAction(nameof(GetWarehouseById), new { id = warehouse.Id }, warehouse);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateWarehouse(int id, Warehouse warehouse)
        {
            if (id != warehouse.Id)
            {
                return BadRequest();
            }

            await _warehouseService.UpdateWarehouseAsync(warehouse);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWarehouse(int id)
        {
            await _warehouseService.DeleteWarehouseAsync(id);
            return NoContent();
        }

        [HttpGet("with-inventories")]
        public async Task<ActionResult<IEnumerable<Warehouse>>> GetAllWarehousesWithInventories()
        {
            var warehouses = await _warehouseService.GetAllWarehousesWithInventoriesAsync();
            return Ok(warehouses);
        }
    }
}
