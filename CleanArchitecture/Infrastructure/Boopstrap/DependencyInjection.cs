using Infrastructure.Persistence;
using Infrastructure.TransactionalRepository.Inmplementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure.Bootstrap
{
  

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomDbContext<EsmeraldadbContext>(configuration);
            return services;
        }
    }

}
