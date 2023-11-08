using Microsoft.Extensions.DependencyInjection;
using Trips.Core.DataService.Trips;
using Trips.Core.Service.Calculation;
using Trips.Data.Entities;

namespace Trips.Domain.Test;

public class CalculationTests
{

    [Fact]
    public async Task GIVEN_DriverWithTrips_WHEN_CalculatePayabletime_THEN_PayableTimeCalculatedCorrectly()
    {
        using var container = new UnitTestContainer();
        var serviceProvider = container.ServiceProvider;
        // GIVEN
        var exprectedMinutes = 5;
        var tripDataService = serviceProvider.GetService<ITripDataService>();
        var calculationService = serviceProvider.GetService<ICalculationService>();

        var now = DateTime.UtcNow;
        var trips = new List<Trip> 
        {
            new Trip 
            {
                Id = 1,
                DriverId = 1,
                Pickup = "2020-11-20 08:00:00",
                Dropoff = "2020-11-20 08:05:00"
            }
        };

        await tripDataService.BulkCreateAsync(trips);
        
        // WHEN
        var result = await calculationService.CalculateDriversPayableTimeAsync();

        // THEN
        var driver = result.FirstOrDefault();
        Assert.Equal(exprectedMinutes, driver.PayableTime);
    }
}