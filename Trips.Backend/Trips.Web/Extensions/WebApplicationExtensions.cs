using Microsoft.EntityFrameworkCore;

namespace Trips.Web.Extensions;

public static class WebApplicationExtensions
{
    public static void MigrateDbContext<TContext>(this WebApplication app) where TContext : DbContext
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<TContext>();
        if (context != null)
        {
            context.Database.Migrate();
        }
    }
}
