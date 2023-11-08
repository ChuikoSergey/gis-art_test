using Microsoft.AspNetCore.Mvc;
using Trips.Core.DataService.Trips;
using Trips.Core.DTO.SearchableRequest;
using Trips.Core.Service.Calculation;

namespace Trips.Web;

[Route("api/[controller]")]
public class DriverController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetDriversTableDataAsync([FromQuery] SearchableRequest request, [FromServices] ITripDataService driverService) 
        => Ok(await driverService.GetDriversTableDataAsync(request));

    [HttpPut("payableTime/calculate")]
    public async Task<IActionResult> CalculatePayableTimeAsync([FromServices] ICalculationService calculationService)
    {
        return Ok(await calculationService.CalculateDriversPayableTimeAsync());
    }
}
