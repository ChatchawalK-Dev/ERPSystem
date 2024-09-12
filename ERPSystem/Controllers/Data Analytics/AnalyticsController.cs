using ERPSystem.Models.Data_Analytics;
using ERPSystem.Services;
using ERPSystem.Services.Data_Analytics;
using Microsoft.AspNetCore.Mvc;

namespace ERPSystem.Controllers.Data_Analytics
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Analytics>> GetById(int id)
        {
            var analytics = await _analyticsService.GetByIdAsync(id);
            if (analytics == null)
            {
                return NotFound();
            }
            return Ok(analytics);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Analytics>>> GetAll()
        {
            var analyticsList = await _analyticsService.GetAllAsync();
            return Ok(analyticsList);
        }

        [HttpPost]
        public async Task<ActionResult<Analytics>> Create(Analytics analytics)
        {
            await _analyticsService.AddAsync(analytics);
            return CreatedAtAction(nameof(GetById), new { id = analytics.Id }, analytics);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Analytics analytics)
        {
            if (id != analytics.Id)
            {
                return BadRequest();
            }

            await _analyticsService.UpdateAsync(analytics);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _analyticsService.DeleteAsync(id);
            return NoContent();
        }
    }
}
