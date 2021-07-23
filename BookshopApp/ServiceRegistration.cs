using BookshopApp.Data;
using BookshopApp.Data.Implementations;
using BookshopApp.Data.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookshopApp
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<DataGenerator>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
