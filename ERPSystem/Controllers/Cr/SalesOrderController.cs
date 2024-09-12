using ERPSystem.Models.Cr;
using ERPSystem.Services.Cr;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Controllers.Cr
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesOrderController : ControllerBase
    {
        private readonly ISalesOrderService _salesOrderService;

        public SalesOrderController(ISalesOrderService salesOrderService)
        {
            _salesOrderService = salesOrderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesOrder>>> GetAllSalesOrders()
        {
            var salesOrders = await _salesOrderService.GetAllSalesOrdersAsync();
            return Ok(salesOrders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalesOrder?>> GetSalesOrderById(int id)
        {
            var salesOrder = await _salesOrderService.GetSalesOrderByIdAsync(id);
            if (salesOrder == null)
            {
                return NotFound();
            }
            return Ok(salesOrder);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSalesOrder(SalesOrder salesOrder)
        {
            await _salesOrderService.CreateSalesOrderAsync(salesOrder);
            return CreatedAtAction(nameof(GetSalesOrderById), new { id = salesOrder.Id }, salesOrder);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSalesOrder(int id, SalesOrder salesOrder)
        {
            if (id != salesOrder.Id)
            {
                return BadRequest();
            }

            await _salesOrderService.UpdateSalesOrderAsync(salesOrder);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSalesOrder(int id)
        {
            await _salesOrderService.DeleteSalesOrderAsync(id);
            return NoContent();
        }

        [HttpGet("byCustomer/{customerId}")]
        public async Task<ActionResult<IEnumerable<SalesOrder>>> GetSalesOrdersByCustomerId(int customerId)
        {
            var salesOrders = await _salesOrderService.GetSalesOrdersByCustomerIdAsync(customerId);
            return Ok(salesOrders);
        }
    }
}
