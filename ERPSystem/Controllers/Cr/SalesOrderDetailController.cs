using ERPSystem.Models.Cr;
using ERPSystem.Services.Cr;
using Microsoft.AspNetCore.Mvc;

namespace ERPSystem.Controllers.Cr
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrderDetailController : ControllerBase
    {
        private readonly ISalesOrderDetailService _salesOrderDetailService;

        public SalesOrderDetailController(ISalesOrderDetailService salesOrderDetailService)
        {
            _salesOrderDetailService = salesOrderDetailService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesOrderDetail>>> GetSalesOrderDetails()
        {
            var salesOrderDetails = await _salesOrderDetailService.GetAllSalesOrderDetailsAsync();
            return Ok(salesOrderDetails);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalesOrderDetail>> GetSalesOrderDetail(int id)
        {
            var salesOrderDetail = await _salesOrderDetailService.GetSalesOrderDetailByIdAsync(id);
            if (salesOrderDetail == null)
            {
                return NotFound();
            }
            return Ok(salesOrderDetail);
        }

        [HttpPost]
        public async Task<ActionResult<SalesOrderDetail>> CreateSalesOrderDetail(SalesOrderDetail salesOrderDetail)
        {
            await _salesOrderDetailService.CreateSalesOrderDetailAsync(salesOrderDetail);
            return CreatedAtAction(nameof(GetSalesOrderDetail), new { id = salesOrderDetail.Id }, salesOrderDetail);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSalesOrderDetail(int id, SalesOrderDetail salesOrderDetail)
        {
            if (id != salesOrderDetail.Id)
            {
                return BadRequest();
            }

            await _salesOrderDetailService.UpdateSalesOrderDetailAsync(salesOrderDetail);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesOrderDetail(int id)
        {
            var salesOrderDetail = await _salesOrderDetailService.GetSalesOrderDetailByIdAsync(id);
            if (salesOrderDetail == null)
            {
                return NotFound();
            }

            await _salesOrderDetailService.DeleteSalesOrderDetailAsync(id);
            return NoContent();
        }
    }
}
