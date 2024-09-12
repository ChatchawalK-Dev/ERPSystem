using ERPSystem.Models.Data_Analytics;
using ERPSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Controllers.Data_Analytics
{

    [ApiController]
    [Route("api/[controller]")]
    public class DataReportController : ControllerBase
    {
        private readonly IDataReportService _dataReportService;

        public DataReportController(IDataReportService dataReportService)
        {
            _dataReportService = dataReportService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataReport>> GetDataReport(int id)
        {
            var dataReport = await _dataReportService.GetDataReportByIdAsync(id);
            if (dataReport == null)
            {
                return NotFound();
            }
            return Ok(dataReport);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataReport>>> GetAllDataReports()
        {
            var dataReports = await _dataReportService.GetAllDataReportsAsync();
            return Ok(dataReports);
        }

        [HttpPost]
        public async Task<ActionResult<DataReport>> CreateDataReport([FromBody] DataReport dataReport)
        {
            await _dataReportService.AddDataReportAsync(dataReport);
            return CreatedAtAction(nameof(GetDataReport), new { id = dataReport.Id }, dataReport);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDataReport(int id, [FromBody] DataReport dataReport)
        {
            if (id != dataReport.Id)
            {
                return BadRequest();
            }
            await _dataReportService.UpdateDataReportAsync(dataReport);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDataReport(int id)
        {
            await _dataReportService.DeleteDataReportAsync(id);
            return NoContent();
        }
    }
}
