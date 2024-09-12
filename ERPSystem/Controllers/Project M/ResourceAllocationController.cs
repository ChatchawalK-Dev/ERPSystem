using ERPSystem.Models.Project_M;
using ERPSystem.Services.ProjectM;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Controllers.ProjectM
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourceAllocationController : ControllerBase
    {
        private readonly IResourceAllocationService _resourceAllocationService;

        public ResourceAllocationController(IResourceAllocationService resourceAllocationService)
        {
            _resourceAllocationService = resourceAllocationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResourceAllocation>>> GetAllAllocations()
        {
            var allocations = await _resourceAllocationService.GetAllAllocationsAsync();
            return Ok(allocations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResourceAllocation?>> GetAllocationById(int id)
        {
            var allocation = await _resourceAllocationService.GetAllocationByIdAsync(id);
            if (allocation == null)
            {
                return NotFound();
            }
            return Ok(allocation);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAllocation(ResourceAllocation resourceAllocation)
        {
            await _resourceAllocationService.CreateAllocationAsync(resourceAllocation);
            return CreatedAtAction(nameof(GetAllocationById), new { id = resourceAllocation.Id }, resourceAllocation);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAllocation(int id, ResourceAllocation resourceAllocation)
        {
            if (id != resourceAllocation.Id)
            {
                return BadRequest();
            }

            await _resourceAllocationService.UpdateAllocationAsync(resourceAllocation);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAllocation(int id)
        {
            await _resourceAllocationService.DeleteAllocationAsync(id);
            return NoContent();
        }

        [HttpGet("project/{projectId}")]
        public async Task<ActionResult<IEnumerable<ResourceAllocation>>> GetAllocationsByProjectId(int projectId)
        {
            var allocations = await _resourceAllocationService.GetAllocationsByProjectIdAsync(projectId);
            return Ok(allocations);
        }

        [HttpGet("resource/{resourceId}")]
        public async Task<ActionResult<IEnumerable<ResourceAllocation>>> GetAllocationsByResourceId(int resourceId)
        {
            var allocations = await _resourceAllocationService.GetAllocationsByResourceIdAsync(resourceId);
            return Ok(allocations);
        }
    }
}
