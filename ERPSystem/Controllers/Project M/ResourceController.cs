using ERPSystem.Models.Project_M;
using ERPSystem.Services.ProjectM;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Controllers.ProjectM
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _resourceService;

        public ResourceController(IResourceService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resource>>> GetAllResources()
        {
            var resources = await _resourceService.GetAllResourcesAsync();
            return Ok(resources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Resource?>> GetResourceById(int id)
        {
            var resource = await _resourceService.GetResourceByIdAsync(id);
            if (resource == null)
            {
                return NotFound();
            }
            return Ok(resource);
        }

        [HttpPost]
        public async Task<ActionResult> CreateResource(Resource resource)
        {
            await _resourceService.CreateResourceAsync(resource);
            return CreatedAtAction(nameof(GetResourceById), new { id = resource.Id }, resource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateResource(int id, Resource resource)
        {
            if (id != resource.Id)
            {
                return BadRequest();
            }

            await _resourceService.UpdateResourceAsync(resource);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteResource(int id)
        {
            await _resourceService.DeleteResourceAsync(id);
            return NoContent();
        }
    }
}
