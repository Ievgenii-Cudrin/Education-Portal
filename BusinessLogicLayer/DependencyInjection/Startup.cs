using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.DependencyInjection
{
    public static class Startup
    {
        public static IServiceProvider ConfigureService()
        {
            var provider = new ServiceCollection()
                .AddSingleton(typeof(IRepository<>), typeof(Repository<>))
                .BuildServiceProvider();

            return provider;
        }
    }
}
