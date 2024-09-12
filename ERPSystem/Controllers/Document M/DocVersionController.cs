using ERPSystem.Models.Document_M;
using ERPSystem.Services.DocumentM;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Controllers.DocumentM
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocVersionController : ControllerBase
    {
        private readonly IDocVersionService _docVersionService;

        public DocVersionController(IDocVersionService docVersionService)
        {
            _docVersionService = docVersionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocVersion>>> GetAllDocVersions()
        {
            var docVersions = await _docVersionService.GetAllDocVersionsAsync();
            return Ok(docVersions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocVersion?>> GetDocVersionById(int id)
        {
            var docVersion = await _docVersionService.GetDocVersionByIdAsync(id);
            if (docVersion == null)
            {
                return NotFound();
            }
            return Ok(docVersion);
        }

        [HttpPost]
        public async Task<ActionResult> CreateDocVersion(DocVersion docVersion)
        {
            await _docVersionService.CreateDocVersionAsync(docVersion);
            return CreatedAtAction(nameof(GetDocVersionById), new { id = docVersion.Id }, docVersion);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDocVersion(int id, DocVersion docVersion)
        {
            if (id != docVersion.Id)
            {
                return BadRequest();
            }

            await _docVersionService.UpdateDocVersionAsync(docVersion);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDocVersion(int id)
        {
            await _docVersionService.DeleteDocVersionAsync(id);
            return NoContent();
        }

        [HttpGet("with-documents")]
        public async Task<ActionResult<IEnumerable<DocVersion>>> GetDocVersionsWithDocuments()
        {
            var docVersions = await _docVersionService.GetDocVersionsWithDocumentsAsync();
            return Ok(docVersions);
        }
    }
}
