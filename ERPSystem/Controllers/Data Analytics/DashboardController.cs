using ERPSystem.Models.Data_Analytics;
using ERPSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERPSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        // GET: api/dashboard
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dashboard>>> GetDashboards()
        {
            var dashboards = await _dashboardService.GetAllDashboardsAsync();
            return Ok(dashboards);
        }

        // GET: api/dashboard/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Dashboard>> GetDashboard(int id)
        {
            var dashboard = await _dashboardService.GetDashboardByIdAsync(id);

            if (dashboard == null)
            {
                return NotFound();
            }

            return Ok(dashboard);
        }

        // POST: api/dashboard
        [HttpPost]
        public async Task<ActionResult<Dashboard>> CreateDashboard([FromBody] Dashboard dashboard)
        {
            if (dashboard == null)
            {
                return BadRequest();
            }

            await _dashboardService.AddDashboardAsync(dashboard);

            return CreatedAtAction(nameof(GetDashboard), new { id = dashboard.Id }, dashboard);
        }

        // PUT: api/dashboard/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDashboard(int id, [FromBody] Dashboard dashboard)
        {
            if (id != dashboard.Id)
            {
                return BadRequest();
            }

            var existingDashboard = await _dashboardService.GetDashboardByIdAsync(id);

            if (existingDashboard == null)
            {
                return NotFound();
            }

            await _dashboardService.UpdateDashboardAsync(dashboard);

            return NoContent();
        }

        // DELETE: api/dashboard/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDashboard(int id)
        {
            var existingDashboard = await _dashboardService.GetDashboardByIdAsync(id);

            if (existingDashboard == null)
            {
                return NotFound();
            }

            await _dashboardService.DeleteDashboardAsync(id);

            return NoContent();
        }
    }
}
