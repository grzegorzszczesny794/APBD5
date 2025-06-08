using Microsoft.AspNetCore.Mvc;
using Tutorial5.DTOs;
using Tutorial5.Services;

namespace Tutorial5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WashingMachinesController
    {
        private readonly IDbService _service;

        public WashingMachinesController(IDbService service)
        {
            _service = service;
        }

    }
}
