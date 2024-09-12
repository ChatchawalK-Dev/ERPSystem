using ERPSystem.Models.Document_M;
using ERPSystem.Services.DocumentM;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Controllers.DocumentM
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccessControlController : ControllerBase
    {
        private readonly IAccessControlService _accessControlService;

        public AccessControlController(IAccessControlService accessControlService)
        {
            _accessControlService = accessControlService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccessControl>>> GetAllAccessControls()
        {
            var accessControls = await _accessControlService.GetAllAccessControlsAsync();
            return Ok(accessControls);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AccessControl?>> GetAccessControlById(int id)
        {
            var accessControl = await _accessControlService.GetAccessControlByIdAsync(id);
            if (accessControl == null)
            {
                return NotFound();
            }
            return Ok(accessControl);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAccessControl(AccessControl accessControl)
        {
            await _accessControlService.CreateAccessControlAsync(accessControl);
            return CreatedAtAction(nameof(GetAccessControlById), new { id = accessControl.Id }, accessControl);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAccessControl(int id, AccessControl accessControl)
        {
            if (id != accessControl.Id)
            {
                return BadRequest();
            }

            await _accessControlService.UpdateAccessControlAsync(accessControl);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAccessControl(int id)
        {
            await _accessControlService.DeleteAccessControlAsync(id);
            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<AccessControl>>> GetAccessControlsByUserId(int userId)
        {
            var accessControls = await _accessControlService.GetAccessControlsByUserIdAsync(userId);
            return Ok(accessControls);
        }

        [HttpGet("document/{documentId}")]
        public async Task<ActionResult<IEnumerable<AccessControl>>> GetAccessControlsByDocumentId(int documentId)
        {
            var accessControls = await _accessControlService.GetAccessControlsByDocumentIdAsync(documentId);
            return Ok(accessControls);
        }
    }
}
