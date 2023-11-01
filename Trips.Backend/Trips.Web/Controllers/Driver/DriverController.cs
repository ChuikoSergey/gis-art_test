using Microsoft.AspNetCore.Mvc;
using Trips.Core.DataService.Driver;
using Trips.Core.DTO.SearchableRequest;
using Trips.Core.Service.Calculation;

namespace Trips.Web;

[Route("api/[controller]")]
public class DriverController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetDriversTableDataAsync([FromQuery]SearchableRequest searchableRequest, [FromServices] IDriverDataService driverService) 
        => Ok(await driverService.GetDriversTableDataAsync(searchableRequest));

    [HttpPut("payableTime/calculate")]
    public async Task<IActionResult> CalculatePayableTimeAsync([FromServices] ICalculationService calculationService)
    {
        await calculationService.CalculateDriversPayableTime();
        return Ok();
    }
}
