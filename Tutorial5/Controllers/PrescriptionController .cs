using Microsoft.AspNetCore.Mvc;
using Tutorial5.DTOs;
using Tutorial5.Helpers;
using Tutorial5.Services;

namespace Tutorial5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController(IDbService dbService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(PrescriptionCreateRequest request)
        {
            var result = await dbService.AddPrescription(request);

            return result.IsFailure 
                   ? BadRequest(result.Value) 
                   : Ok(result.Error);
        }
    }
}
