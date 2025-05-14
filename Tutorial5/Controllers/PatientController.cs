using Microsoft.AspNetCore.Mvc;
using Tutorial5.Services;

namespace Tutorial5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController(IDbService _dbService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int IdPatient)
        {
            var result = await _dbService.GetPatientInfo(IdPatient);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}
