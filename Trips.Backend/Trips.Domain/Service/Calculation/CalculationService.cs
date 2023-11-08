using Microsoft.Extensions.Options;
using Trips.Core.DataService.Trips;
using Trips.Core.DTO.Driver;
using Trips.Core.DTO.Trip;
using Trips.Core.Service.Calculation;
using Trips.Domain.Configuration;

namespace Trips.Domain.Service.Calculation;

public class CalculationService : ICalculationService
{
    private readonly ITripDataService _tripDataService;
    private readonly int _batchSize;

    public CalculationService(
        ITripDataService tripDataService,
        IOptions<BatchSizeConfiguration> batchSizeConfiguration)
    {
        _tripDataService = tripDataService;
        _batchSize = batchSizeConfiguration.Value.DriverPayableTimeCalculation;
    }

    public async Task<List<DriverListTableDto>> CalculateDriversPayableTimeAsync()
    {
        var drivers = await _tripDataService.GetAllDriversIds();
        var payableTimes = await _tripDataService.GetTripTimeByDrivers(drivers);
        var groupedPayableTimes = payableTimes.GroupBy(pt => pt.DriverId).ToDictionary(g => g.Key);
        var calculationTasks = drivers.Chunk(_batchSize).Select(c => CalculateDriversPayableTimeAsync(c, groupedPayableTimes));
        var tasksResult = await Task.WhenAll(calculationTasks);
        var updatedDrivers = tasksResult.SelectMany(result => result).ToList();
        return updatedDrivers;
    }

    private async Task<IEnumerable<DriverListTableDto>> CalculateDriversPayableTimeAsync(
        IEnumerable<int> driverIds,
        IDictionary<int, IGrouping<int, DriverTripTimeDto>> groupedPayableTimes)
    {
        var result = new List<DriverListTableDto>();
        foreach (var driverId in driverIds) 
        {
            var driver = new DriverListTableDto();
            driver.Id = driverId;
            driver.PayableTime = groupedPayableTimes[driver.Id].Sum(pt => 
            {
                var pickup = DateTime.Parse(pt.Pickup);
                var dropof = DateTime.Parse(pt.Dropoff);
                return (dropof - pickup).TotalMinutes;
            });
            result.Add(driver);
        }
        return result;
    }
}
