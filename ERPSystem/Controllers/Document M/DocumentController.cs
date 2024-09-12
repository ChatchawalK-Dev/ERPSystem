using ERPSystem.Models.Document_M;
using ERPSystem.Services.DocumentM;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Controllers.DocumentM
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetAllDocuments()
        {
            var documents = await _documentService.GetAllDocumentsAsync();
            return Ok(documents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Document?>> GetDocumentById(int id)
        {
            var document = await _documentService.GetDocumentByIdAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            return Ok(document);
        }

        [HttpPost]
        public async Task<ActionResult> CreateDocument(Document document)
        {
            await _documentService.CreateDocumentAsync(document);
            return CreatedAtAction(nameof(GetDocumentById), new { id = document.Id }, document);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDocument(int id, Document document)
        {
            if (id != document.Id)
            {
                return BadRequest();
            }

            await _documentService.UpdateDocumentAsync(document);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDocument(int id)
        {
            await _documentService.DeleteDocumentAsync(id);
            return NoContent();
        }

        [HttpGet("with-access-controls")]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocumentsWithAccessControls()
        {
            var documents = await _documentService.GetDocumentsWithAccessControlsAsync();
            return Ok(documents);
        }

        [HttpGet("with-versions")]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocumentsWithVersions()
        {
            var documents = await _documentService.GetDocumentsWithVersionsAsync();
            return Ok(documents);
        }
    }
}
