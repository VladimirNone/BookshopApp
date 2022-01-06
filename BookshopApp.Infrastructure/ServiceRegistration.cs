using BookshopApp.Infrastructure.Generator;
using BookshopApp.Infrastructure.Implementations;
using BookshopApp.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopApp.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<DataGenerator>();
            services.AddTransient<IFileHandler, FileHandler>();
        }
        
    }
}
