using ERPSystem.Models.HumanResources;
using ERPSystem.Services.HumanResources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Controllers.HumanResources
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecruitmentController : ControllerBase
    {
        private readonly IRecruitmentService _recruitmentService;

        public RecruitmentController(IRecruitmentService recruitmentService)
        {
            _recruitmentService = recruitmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recruitment>>> GetAllRecruitments()
        {
            var recruitments = await _recruitmentService.GetAllRecruitmentsAsync();
            return Ok(recruitments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Recruitment?>> GetRecruitmentById(int id)
        {
            var recruitment = await _recruitmentService.GetRecruitmentByIdAsync(id);
            if (recruitment == null)
            {
                return NotFound();
            }
            return Ok(recruitment);
        }

        [HttpPost]
        public async Task<ActionResult> CreateRecruitment(Recruitment recruitment)
        {
            await _recruitmentService.CreateRecruitmentAsync(recruitment);
            return CreatedAtAction(nameof(GetRecruitmentById), new { id = recruitment.Id }, recruitment);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRecruitment(int id, Recruitment recruitment)
        {
            if (id != recruitment.Id)
            {
                return BadRequest();
            }

            await _recruitmentService.UpdateRecruitmentAsync(recruitment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecruitment(int id)
        {
            await _recruitmentService.DeleteRecruitmentAsync(id);
            return NoContent();
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Recruitment>>> GetRecruitmentsByStatus(string status)
        {
            var recruitments = await _recruitmentService.GetRecruitmentsByStatusAsync(status);
            return Ok(recruitments);
        }
    }
}
