using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Interfaces;
using Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hubtel.eCommerce.Cart.Api.Infrastructure.Persistence
{
	public static class DependencyInjection
	{

		public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IItemRepository, ItemRepository>();

            services.AddMediatR(typeof(Startup));

            return services;
		}
		public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
		{
            var assemblyName = typeof(ECommerceDbContext).Assembly.FullName;

            _= services.AddDbContext<ECommerceDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b =>
                    {
                        b.MigrationsAssembly(assemblyName);
                    }));

            _= services.AddScoped<ECommerceDbContext>();

            return services;
        }
	}
}
