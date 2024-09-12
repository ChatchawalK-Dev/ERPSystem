using ERPSystem.Models.Supply_Chain;
using ERPSystem.Services.SupplyChain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Controllers.SupplyChain
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly IPurchaseOrderService _purchaseOrderService;

        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseOrder>>> GetAllPurchaseOrders()
        {
            var purchaseOrders = await _purchaseOrderService.GetAllPurchaseOrdersAsync();
            return Ok(purchaseOrders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrder?>> GetPurchaseOrderById(int id)
        {
            var purchaseOrder = await _purchaseOrderService.GetPurchaseOrderByIdAsync(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }
            return Ok(purchaseOrder);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePurchaseOrder(PurchaseOrder purchaseOrder)
        {
            await _purchaseOrderService.CreatePurchaseOrderAsync(purchaseOrder);
            return CreatedAtAction(nameof(GetPurchaseOrderById), new { id = purchaseOrder.Id }, purchaseOrder);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePurchaseOrder(int id, PurchaseOrder purchaseOrder)
        {
            if (id != purchaseOrder.Id)
            {
                return BadRequest();
            }

            await _purchaseOrderService.UpdatePurchaseOrderAsync(purchaseOrder);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePurchaseOrder(int id)
        {
            await _purchaseOrderService.DeletePurchaseOrderAsync(id);
            return NoContent();
        }

        [HttpGet("supplier/{supplierId}")]
        public async Task<ActionResult<IEnumerable<PurchaseOrder>>> GetPurchaseOrdersBySupplierId(int supplierId)
        {
            var purchaseOrders = await _purchaseOrderService.GetPurchaseOrdersBySupplierIdAsync(supplierId);
            return Ok(purchaseOrders);
        }
    }
}
