using ERPSystem.Models.Supply_Chain;
using ERPSystem.Services.SupplyChain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Controllers.SupplyChain
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseOrderDetailController : ControllerBase
    {
        private readonly IPurchaseOrderDetailService _purchaseOrderDetailService;

        public PurchaseOrderDetailController(IPurchaseOrderDetailService purchaseOrderDetailService)
        {
            _purchaseOrderDetailService = purchaseOrderDetailService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseOrderDetail>>> GetAllPurchaseOrderDetails()
        {
            var purchaseOrderDetails = await _purchaseOrderDetailService.GetAllPurchaseOrderDetailsAsync();
            return Ok(purchaseOrderDetails);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrderDetail?>> GetPurchaseOrderDetailById(int id)
        {
            var purchaseOrderDetail = await _purchaseOrderDetailService.GetPurchaseOrderDetailByIdAsync(id);
            if (purchaseOrderDetail == null)
            {
                return NotFound();
            }
            return Ok(purchaseOrderDetail);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePurchaseOrderDetail(PurchaseOrderDetail purchaseOrderDetail)
        {
            await _purchaseOrderDetailService.CreatePurchaseOrderDetailAsync(purchaseOrderDetail);
            return CreatedAtAction(nameof(GetPurchaseOrderDetailById), new { id = purchaseOrderDetail.Id }, purchaseOrderDetail);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePurchaseOrderDetail(int id, PurchaseOrderDetail purchaseOrderDetail)
        {
            if (id != purchaseOrderDetail.Id)
            {
                return BadRequest();
            }

            await _purchaseOrderDetailService.UpdatePurchaseOrderDetailAsync(purchaseOrderDetail);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePurchaseOrderDetail(int id)
        {
            await _purchaseOrderDetailService.DeletePurchaseOrderDetailAsync(id);
            return NoContent();
        }

        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<IEnumerable<PurchaseOrderDetail>>> GetPurchaseOrderDetailsByOrderId(int orderId)
        {
            var purchaseOrderDetails = await _purchaseOrderDetailService.GetPurchaseOrderDetailsByOrderIdAsync(orderId);
            return Ok(purchaseOrderDetails);
        }
    }
}
