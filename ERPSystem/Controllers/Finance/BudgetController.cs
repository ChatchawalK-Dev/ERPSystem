using ERPSystem.Models.Finance;
using ERPSystem.Services.Finance;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Controllers.Finance
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Budget>>> GetAllBudgets()
        {
            var budgets = await _budgetService.GetAllBudgetsAsync();
            return Ok(budgets);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Budget?>> GetBudgetById(int id)
        {
            var budget = await _budgetService.GetBudgetByIdAsync(id);
            if (budget == null)
            {
                return NotFound();
            }
            return Ok(budget);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBudget(Budget budget)
        {
            await _budgetService.CreateBudgetAsync(budget);
            return CreatedAtAction(nameof(GetBudgetById), new { id = budget.Id }, budget);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBudget(int id, Budget budget)
        {
            if (id != budget.Id)
            {
                return BadRequest();
            }

            await _budgetService.UpdateBudgetAsync(budget);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBudget(int id)
        {
            await _budgetService.DeleteBudgetAsync(id);
            return NoContent();
        }

        [HttpGet("dateRange")]
        public async Task<ActionResult<IEnumerable<Budget>>> GetBudgetsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var budgets = await _budgetService.GetBudgetsByDateRangeAsync(startDate, endDate);
            return Ok(budgets);
        }
    }
}
