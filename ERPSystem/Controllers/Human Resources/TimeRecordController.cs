using ERPSystem.Models.HumanResources;
using ERPSystem.Services.HumanResources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Controllers.HumanResources
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeRecordController : ControllerBase
    {
        private readonly ITimeRecordService _timeRecordService;

        public TimeRecordController(ITimeRecordService timeRecordService)
        {
            _timeRecordService = timeRecordService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeRecord>>> GetAllTimeRecords()
        {
            var timeRecords = await _timeRecordService.GetAllTimeRecordsAsync();
            return Ok(timeRecords);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TimeRecord?>> GetTimeRecordById(int id)
        {
            var timeRecord = await _timeRecordService.GetTimeRecordByIdAsync(id);
            if (timeRecord == null)
            {
                return NotFound();
            }
            return Ok(timeRecord);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTimeRecord(TimeRecord timeRecord)
        {
            await _timeRecordService.CreateTimeRecordAsync(timeRecord);
            return CreatedAtAction(nameof(GetTimeRecordById), new { id = timeRecord.Id }, timeRecord);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTimeRecord(int id, TimeRecord timeRecord)
        {
            if (id != timeRecord.Id)
            {
                return BadRequest();
            }

            await _timeRecordService.UpdateTimeRecordAsync(timeRecord);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTimeRecord(int id)
        {
            await _timeRecordService.DeleteTimeRecordAsync(id);
            return NoContent();
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<TimeRecord>>> GetTimeRecordsByEmployeeId(int employeeId)
        {
            var timeRecords = await _timeRecordService.GetTimeRecordsByEmployeeIdAsync(employeeId);
            return Ok(timeRecords);
        }
    }
}
