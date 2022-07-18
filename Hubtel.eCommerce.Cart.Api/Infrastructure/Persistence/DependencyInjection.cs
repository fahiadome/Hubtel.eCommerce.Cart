using System;
using System.Linq;
using System.Reflection;
using Hubtel.eCommerce.Cart.Api.Core.Processors;
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
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IItemCategoryRepository, ItemCategoryRepository>();
            services.AddScoped<IItemVendorRepository, ItemVendorRepository>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //services.AddMediatR(typeof(Startup));

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

        public static IServiceCollection AddProcessors(this IServiceCollection services)
        {

            var attributes = typeof(ProcessorBase);
            var assemblies = attributes.Assembly;
            var definedTypes = assemblies.DefinedTypes;

            var processors = definedTypes.Where(type => type.GetTypeInfo().GetCustomAttribute<ProcessorBase>() != null)
                .ToList();


            foreach (var processor in processors)
                services.AddScoped(processor);

            return services;
        }

    }
}
