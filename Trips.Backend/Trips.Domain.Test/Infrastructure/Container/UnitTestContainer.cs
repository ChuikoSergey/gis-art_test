using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Trips.Core.Data.Repository;
using Trips.Core.DataService.BaseCrudService;
using Trips.Core.Service.Calculation;
using Trips.Data.Context;
using Trips.Data.Repository;
using Trips.Domain.Configuration;
using Trips.Domain.DataService.Base;
using Trips.Domain.Service.Calculation;

namespace Trips.Domain.Test;

public class UnitTestContainer : IDisposable
{
    private bool _disposed;
    public ServiceProvider ServiceProvider { get; }

    public UnitTestContainer()
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);
        var configuration = configurationBuilder.Build();

        var services = new ServiceCollection();
        services.AddDbContext<SysContext>(opt => opt.UseInMemoryDatabase(Guid.NewGuid().ToString()));
        services.Add(new ServiceDescriptor(typeof(IRepository<>), typeof(Repository<>), ServiceLifetime.Scoped));
        AddDataServices(services);
        services.Add(new ServiceDescriptor(typeof(ICalculationService), typeof(CalculationService), ServiceLifetime.Transient));
        services.AddOptions();
        services.Configure<BatchSizeConfiguration>(configuration.GetSection("BatchSize"));
        ServiceProvider = services.BuildServiceProvider();
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            ServiceProvider.Dispose();
        }
        _disposed = true;
    }

    private void AddDataServices(IServiceCollection services)
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
                services.Add(new ServiceDescriptor(serviceInterface, dataServiceImplementationType, ServiceLifetime.Transient));
            }
        }
    }
}
