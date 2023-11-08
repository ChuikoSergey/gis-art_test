using Trips.Core.DTO.Driver;

namespace Trips.Core.Service.Calculation;

public interface ICalculationService
{
    Task<List<DriverListTableDto>> CalculateDriversPayableTimeAsync();
}
