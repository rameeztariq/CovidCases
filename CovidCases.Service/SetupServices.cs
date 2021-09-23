using CovidCases.Contract;
using CovidCases.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidCases.Service
{
    public static class SetupServices
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUsersService, UsersService>();
            return services;
        }

    }
}
