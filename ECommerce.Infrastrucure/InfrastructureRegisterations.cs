using ECommerce.Core.Interfaces;
using ECommerce.Core.Services;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Infrastructure.Repositories.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using StackExchange.Redis;

namespace ECommerce.Infrastructure
{
    public static class InfrastructureRegisterations
    {
        public static IServiceCollection AddInfrastructureRegisterations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<AppDbContext>(options
                => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddSingleton<IImageManagementService, ImageManagementService>();
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            // apply Redis Connection
            services.AddSingleton<IConnectionMultiplexer>(i =>
            {
                var config = ConfigurationOptions.Parse(configuration.GetConnectionString("redis"));
                return ConnectionMultiplexer.Connect(config);
            });

            return services;
        }
    }
}
