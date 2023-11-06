using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Booking.Infrastructure.Persistence;
using Taxi.Dictionary.Infrastructure;

namespace Booking.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookingDbContext>(options =>options.UseSqlServer(
               configuration.GetConnectionString("BookingConnection"),
               b => b.MigrationsAssembly(typeof(BookingDbContext).Assembly.FullName)));
            
            services.Scan(scan => scan.FromAssemblies(typeof(IInfrastructureAssemblyMarker).Assembly)
               .AddClasses(@class =>
                     @class.Where(type =>
                           !type.Name.StartsWith('I')
                           && type.Name.EndsWith("Repository")
                            )
                  ).AsSelfWithInterfaces().WithScopedLifetime());
        }
    }
}
