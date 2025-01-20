using Infrastructure.TransactionalRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.TransactionalRepository.Inmplementations
{
    public static class DbContextExtensions
    {
        public static IServiceCollection AddCustomDbContext<TContext>(
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionStringName = "DefaultConnection")
            where TContext : DbContext
        {
            // Obtener connection string
            //var connectionString = configuration.GetConnectionString(connectionStringName);
            //ArgumentNullException.ThrowIfNull(connectionString, nameof(connectionStringName));

            // Registrar DbContext
            services.AddDbContext<TContext>(options =>
                options.UseInMemoryDatabase("TestDatabase"));

            // Registrar las configuraciones necesarias para el repositorio
            services.AddScoped<ITransactionalConfiguration>(provider =>
                new TransactionalConfiguration(
                    provider.GetRequiredService<ILogger<TransactionalRepository>>(),
                    provider.GetRequiredService<TContext>()));

            return services;
        }
    }
}
