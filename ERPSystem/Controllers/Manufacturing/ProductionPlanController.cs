using ERPSystem.Models.Manufacturing;
using ERPSystem.Services.Manufacturing;
using Microsoft.AspNetCore.Mvc;

namespace ERPSystem.Controllers.Manufacturing
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionPlansController : ControllerBase
    {
        private readonly IProductionPlanService _service;

        public ProductionPlansController(IProductionPlanService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductionPlan>>> GetProductionPlans()
        {
            var productionPlans = await _service.GetAllAsync();
            return Ok(productionPlans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductionPlan>> GetProductionPlan(int id)
        {
            var productionPlan = await _service.GetByIdAsync(id);

            if (productionPlan == null)
            {
                return NotFound();
            }

            return Ok(productionPlan);
        }

        [HttpPost]
        public async Task<ActionResult<ProductionPlan>> PostProductionPlan(ProductionPlan productionPlan)
        {
            await _service.AddAsync(productionPlan);
            return CreatedAtAction(nameof(GetProductionPlan), new { id = productionPlan.Id }, productionPlan);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductionPlan(int id, ProductionPlan productionPlan)
        {
            if (id != productionPlan.Id)
            {
                return BadRequest();
            }

            var existingPlan = await _service.GetByIdAsync(id);
            if (existingPlan == null)
            {
                return NotFound();
            }

            try
            {
                await _service.UpdateAsync(productionPlan);
            }
            catch (Exception)
            {
                // If the production plan does not exist, return NotFound.
                if (await _service.GetByIdAsync(id) == null)
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductionPlan(int id)
        {
            var productionPlan = await _service.GetByIdAsync(id);
            if (productionPlan == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}
