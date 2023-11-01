using Microsoft.Extensions.Options;
using Trips.Core;
using Trips.Core.DataService.Driver;
using Trips.Core.DataService.Trip;
using Trips.Core.Entities;
using Trips.Core.Service.Calculation;
using Trips.Domain.Configuration;

namespace Trips.Domain.Service.Calculation;

public class CalculationService : ICalculationService
{
    private readonly IDriverDataService _driverDataService;
    private readonly ITripDataService _tripDataService;
    private readonly int _batchSize;

    public CalculationService(
        IDriverDataService driverDataService,
        ITripDataService tripDataService,
        IOptions<BatchSizeConfiguration> batchSizeConfiguration)
    {
        _driverDataService = driverDataService;
        _tripDataService = tripDataService;
        _batchSize = batchSizeConfiguration.Value.DriverPayableTimeCalculation;
    }

    public async Task CalculateDriversPayableTime()
    {
        var drivers = await _driverDataService.GetAllAsync();
        var payableTimes = await _tripDataService.GetTripTimeByDrivers(drivers.Select(d => d.Id));
        var groupedPayableTimes = payableTimes.GroupBy(pt => pt.DriverId).ToDictionary(g => g.Key);
        var calculationTasks = drivers.Chunk(_batchSize).Select(c => CalculateDriversPayableTime(c, groupedPayableTimes));
        var tasksResult = await Task.WhenAll(calculationTasks);
        var updatedDrivers = tasksResult.SelectMany(result => result);
        await _driverDataService.BulkUpdateAsync(updatedDrivers);
    }

    private async Task<IEnumerable<Driver>> CalculateDriversPayableTime(
        IEnumerable<Driver> drivers,
        IDictionary<Guid, IGrouping<Guid, DriverTripTimeDto>> groupedPayableTimes)
    {
        foreach (var driver in drivers)
        {
            driver.PayableTime = groupedPayableTimes[driver.Id].Sum(pt => (pt.TripEndTimestamp - pt.TripStartTimestamp).TotalMinutes);
        }
        return drivers;
    }
}
