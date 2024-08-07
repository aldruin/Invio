﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Application.Configuracoes
{
    public static class ConfigurationModule
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services,
                IConfiguration configuration)
        {
            services.AddHttpClient();

            services.AddControllers();

            var info = new OpenApiInfo();
            info.Version = "V1";
            info.Title = "API Invio";


            return services;
        }
    }
}