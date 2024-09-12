using ERPSystem.Models.Manufacturing;
using ERPSystem.Services.Manufacturing;
using Microsoft.AspNetCore.Mvc;

namespace ERPSystem.Controllers.Manufacturing
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualityControlController : ControllerBase
    {
        private readonly IQualityControlService _qualityControlService;

        public QualityControlController(IQualityControlService qualityControlService)
        {
            _qualityControlService = qualityControlService;
        }

        // GET: api/QualityControl
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QualityControl>>> GetAllItems()
        {
            var items = await _qualityControlService.GetAllItemsAsync();
            return Ok(items);
        }

        // GET: api/QualityControl/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QualityControl>> GetItemById(int id)
        {
            var item = await _qualityControlService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST: api/QualityControl
        [HttpPost]
        public async Task<ActionResult<QualityControl>> AddItem(QualityControl item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            await _qualityControlService.AddItemAsync(item);
            return CreatedAtAction(nameof(GetItemById), new { id = item.Id }, item);
        }

        // PUT: api/QualityControl/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, QualityControl item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            var existingItem = await _qualityControlService.GetItemByIdAsync(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            await _qualityControlService.UpdateItemAsync(item);
            return NoContent();
        }

        // DELETE: api/QualityControl/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var existingItem = await _qualityControlService.GetItemByIdAsync(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            await _qualityControlService.DeleteItemAsync(id);
            return NoContent();
        }

        // GET: api/QualityControl/search
        [HttpGet("search")]
        public async Task<ActionResult<QualityControl>> FindItem([FromQuery] string status)
        {
            var item = await _qualityControlService.FindItemAsync(qc => qc.Status == status);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }
    }
}
