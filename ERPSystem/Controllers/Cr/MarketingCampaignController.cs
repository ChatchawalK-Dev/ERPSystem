using ERPSystem.Models.Cr;
using ERPSystem.Services.Cr;
using Microsoft.AspNetCore.Mvc;

namespace ERPSystem.Controllers.Cr
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarketingCampaignController : ControllerBase
    {
        private readonly IMarketingCampaignService _marketingCampaignService;

        public MarketingCampaignController(IMarketingCampaignService marketingCampaignService)
        {
            _marketingCampaignService = marketingCampaignService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MarketingCampaign>> GetById(int id)
        {
            var marketingCampaign = await _marketingCampaignService.GetByIdAsync(id);
            if (marketingCampaign == null)
            {
                return NotFound();
            }
            return Ok(marketingCampaign);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarketingCampaign>>> GetAll()
        {
            var marketingCampaigns = await _marketingCampaignService.GetAllAsync();
            return Ok(marketingCampaigns);
        }

        [HttpPost]
        public async Task<ActionResult<MarketingCampaign>> Create(MarketingCampaign marketingCampaign)
        {
            await _marketingCampaignService.AddAsync(marketingCampaign);
            return CreatedAtAction(nameof(GetById), new { id = marketingCampaign.Id }, marketingCampaign);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MarketingCampaign marketingCampaign)
        {
            if (id != marketingCampaign.Id)
            {
                return BadRequest();
            }

            await _marketingCampaignService.UpdateAsync(marketingCampaign);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _marketingCampaignService.DeleteAsync(id);
            return NoContent();
        }
    }
}
