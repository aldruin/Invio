using Invio.Domain.Entities;
using Invio.Domain.Repositories;
using Invio.Infrastructure.Data;
using Invio.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Infrastructure.Configuracoes
{
    public static class ConfigurationModule
    {
        public static IServiceCollection RegisterRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(Repository<>));
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IEquipeRepository, EquipeRepository>();

            return services;
        }
    }
}
