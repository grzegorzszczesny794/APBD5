using Microsoft.AspNetCore.Mvc;
using Tutorial5.DTOs;
using Tutorial5.Services;


[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IDbService _service;

    public CustomersController(IDbService service)
    {
        _service = service;
    }

    [HttpGet("{customerId}/purchases")]
    public async Task<ActionResult<CustomerResponse>> GetCustomerPurchases([FromQuery] int customerId)
    {
       var result = await _service.GetCustomerInfo(customerId);

        return result.IsFailure
               ? BadRequest(result.Error)
               : Ok(result.Value);
    }
}
