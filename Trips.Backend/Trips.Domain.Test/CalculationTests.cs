using Microsoft.Extensions.DependencyInjection;
using Trips.Core.DataService.Driver;
using Trips.Core.DataService.Passenger;
using Trips.Core.DataService.Trip;
using Trips.Core.DataService.TripPassenger;
using Trips.Core.Entities;
using Trips.Core.Service.Calculation;

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
        var driverDataService = serviceProvider.GetService<IDriverDataService>();
        var tripDataService = serviceProvider.GetService<ITripDataService>();
        var passengerDataService = serviceProvider.GetService<IPassengerDataService>();
        var tripPassangerDataService = serviceProvider.GetService<ITripPassengerDataService>();
        var calculationService = serviceProvider.GetService<ICalculationService>();
        var driver = new Driver
        {
            Name = "Driver1"
        };
        await driverDataService.CreateAsync(driver);

        var now = DateTime.UtcNow;
        var trips = new List<Trip> 
        {
            new Trip 
            {
                DriverId = driver.Id,
                TripStartTimestamp = now,
                TripEndTimestamp = now.AddMinutes(5)
            },
            new Trip
            {
                DriverId = driver.Id,
                TripStartTimestamp = now.AddMinutes(5),
                TripEndTimestamp = now.AddMinutes(10)
            }
        };

        await tripDataService.BulkCreateAsync(trips);

        var passengers = new List<Passenger>
        {
            new Passenger 
            {
                Name = "Passenger A"
            }
        };
        await passengerDataService.BulkCreateAsync(passengers);

        var tripPassengers = new List<TripPassenger>
        {
            new TripPassenger 
            {
                TripId = trips.FirstOrDefault().Id,
                PassengerId = passengers.FirstOrDefault().Id
            }
        };
        await tripPassangerDataService.BulkCreateAsync(tripPassengers);
        
        // WHEN
        await calculationService.CalculateDriversPayableTime();

        // THEN
        driver = await driverDataService.GetByIdAsync(driver.Id);
        Assert.Equal(exprectedMinutes, driver.PayableTime);
    }
}