using ERPSystem.Models.Supply_Chain;
using ERPSystem.Services.SupplyChain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Controllers.SupplyChain
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetAllDeliveries()
        {
            var deliveries = await _deliveryService.GetAllDeliveriesAsync();
            return Ok(deliveries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Delivery?>> GetDeliveryById(int id)
        {
            var delivery = await _deliveryService.GetDeliveryByIdAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }
            return Ok(delivery);
        }

        [HttpPost]
        public async Task<ActionResult> CreateDelivery(Delivery delivery)
        {
            await _deliveryService.CreateDeliveryAsync(delivery);
            return CreatedAtAction(nameof(GetDeliveryById), new { id = delivery.Id }, delivery);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDelivery(int id, Delivery delivery)
        {
            if (id != delivery.Id)
            {
                return BadRequest();
            }

            await _deliveryService.UpdateDeliveryAsync(delivery);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDelivery(int id)
        {
            await _deliveryService.DeleteDeliveryAsync(id);
            return NoContent();
        }

        [HttpGet("purchase-order/{purchaseOrderId}")]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveriesByPurchaseOrderId(int purchaseOrderId)
        {
            var deliveries = await _deliveryService.GetDeliveriesByPurchaseOrderIdAsync(purchaseOrderId);
            return Ok(deliveries);
        }
    }
}
