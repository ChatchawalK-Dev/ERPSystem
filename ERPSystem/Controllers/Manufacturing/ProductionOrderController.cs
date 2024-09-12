using ERPSystem.Models.Manufacturing;
using ERPSystem.Services.Manufacturing;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Controllers.Manufacturing
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionOrderController : ControllerBase
    { 
        private readonly IProductionOrderService _service;

        public ProductionOrderController(IProductionOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductionOrder>>> GetAll()
        {
            var orders = await _service.GetAllAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductionOrder>> GetById(int id)
        {
            var order = await _service.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] ProductionOrder? productionOrder)
        {
            if (productionOrder == null)
            {
                return BadRequest();
            }

            await _service.AddAsync(productionOrder);
            return CreatedAtAction(nameof(GetById), new { id = productionOrder.Id }, productionOrder);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] ProductionOrder productionOrder)
        {
            if (id != productionOrder.Id)
            {
                return BadRequest();
            }

            var existingOrder = await _service.GetByIdAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            await _service.UpdateAsync(productionOrder);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existingOrder = await _service.GetByIdAsync(id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
