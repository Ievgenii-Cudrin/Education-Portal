using DataAccessLayer.DataContext;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using DataAccessLayer.Serialization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DependencyInjection
{
    public static class Startup
    {
        public static IServiceProvider ConfigureService()
        {
            var provider = new ServiceCollection()
                .AddSingleton(typeof(IXmlSet<>), typeof(XmlSet<>))
                .AddSingleton(typeof(IXmlSerializeContext<>), typeof(XmlSerializationContextGeneric<>))
                .BuildServiceProvider();

            return provider;
        }
    }
}
