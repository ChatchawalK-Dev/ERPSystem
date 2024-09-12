using ERPSystem.Models.HumanResources;
using ERPSystem.Services.HumanResources;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERPSystem.Controllers.HumanResources
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingService _trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Training>>> GetAllTrainings()
        {
            var trainings = await _trainingService.GetAllTrainingsAsync();
            return Ok(trainings);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Training?>> GetTrainingById(int id)
        {
            var training = await _trainingService.GetTrainingByIdAsync(id);
            if (training == null)
            {
                return NotFound();
            }
            return Ok(training);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTraining(Training training)
        {
            await _trainingService.CreateTrainingAsync(training);
            return CreatedAtAction(nameof(GetTrainingById), new { id = training.Id }, training);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTraining(int id, Training training)
        {
            if (id != training.Id)
            {
                return BadRequest();
            }

            await _trainingService.UpdateTrainingAsync(training);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTraining(int id)
        {
            await _trainingService.DeleteTrainingAsync(id);
            return NoContent();
        }

        [HttpGet("employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<Training>>> GetTrainingsByEmployeeId(int employeeId)
        {
            var trainings = await _trainingService.GetTrainingsByEmployeeIdAsync(employeeId);
            return Ok(trainings);
        }
    }
}
