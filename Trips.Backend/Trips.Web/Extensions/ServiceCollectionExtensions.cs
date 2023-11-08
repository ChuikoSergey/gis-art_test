using Microsoft.EntityFrameworkCore;
using Trips.Core.Data.Repository;
using Trips.Core.DataService.BaseCrudService;
using Trips.Core.Service.Calculation;
using Trips.Data.Context;
using Trips.Data.Repository;
using Trips.Domain.Configuration;
using Trips.Domain.DataService.Base;
using Trips.Domain.Service.Calculation;

namespace Trips.Web;

public static class ServiceCollectionExtensions
{
    public static void AddDataContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<SysContext>(options => 
        {
            options.UseMySql(
                builder.Configuration["Database:ConnectionString"],
                new MySqlServerVersion(new Version(builder.Configuration["Database:Version"])));
            options.EnableSensitiveDataLogging(true);
        });
    }

    public static void AddDataDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.Add(new ServiceDescriptor(typeof(IRepository<>), typeof(Repository<>), ServiceLifetime.Scoped));
    }

    public static void AddDataServices(this WebApplicationBuilder builder)
    {
        var domainAssembly = typeof(BaseDataService<>).Assembly;
        var interfacesAssembly = typeof(IBaseDataService<>).Assembly;
        var domainAssemblyTypes = domainAssembly.DefinedTypes;
        var interfacesAssemblyTypes = interfacesAssembly.DefinedTypes;
        var servicesInterfaces = interfacesAssemblyTypes.Where(t =>
        {
            return t.IsInterface && t.ImplementedInterfaces.Any(ii =>
            {
                return ii.IsGenericType && ii.GetGenericTypeDefinition() == typeof(IBaseDataService<>);
            });
        });
        foreach (var serviceInterface in servicesInterfaces)
        {
            var dataServiceImplementationType = domainAssemblyTypes.FirstOrDefault(t => t.ImplementedInterfaces.Any(ii => ii == serviceInterface));
            if (dataServiceImplementationType != null)
            {
                builder.Services.Add(new ServiceDescriptor(serviceInterface, dataServiceImplementationType, ServiceLifetime.Transient));
            }
        }
    }

    public static void AddNonDataService(this WebApplicationBuilder builder)
    {
        builder.Services.Add(new ServiceDescriptor(typeof(ICalculationService), typeof(CalculationService), ServiceLifetime.Transient));
    }

    public static void AddConfigurations(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions();
        builder.Services.Configure<BatchSizeConfiguration>(builder.Configuration.GetSection("BatchSize"));
    }
}
