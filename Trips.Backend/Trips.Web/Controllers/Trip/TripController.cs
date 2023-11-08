using Microsoft.AspNetCore.Mvc;
using Trips.Core.DataService.Trips;
using Trips.Core.DTO.SearchableRequest.Trip;

namespace Trips.Web;

[Route("api/[controller]")]
public class TripController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTripsTableDataByDriverAsync([FromQuery]TripTableDtoRequest? request, [FromServices] ITripDataService tripDataService) 
        => Ok(await tripDataService.GetTableDataByDriverAsync(request));
}
