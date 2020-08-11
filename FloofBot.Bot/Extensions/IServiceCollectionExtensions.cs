using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FloofBot.Bot.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FloofBot.Bot.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void LoadFrom(this IServiceCollection serviceCollection, Assembly assembly)
        {
            Queue<Type> services = new Queue<Type>(assembly.GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IFloofyService))
                            && !x.GetTypeInfo().IsInterface && !x.GetTypeInfo().IsAbstract));
            
            HashSet<Type> interfaces = new HashSet<Type>(assembly.GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IFloofyService)) && x.GetTypeInfo().IsInterface));

            while (services.Any())
            {
                Type serviceType = services.Dequeue();

                if (serviceCollection.FirstOrDefault(x => x.ServiceType == serviceType) != null)
                {
                    continue;
                }

                Type interfaceType = interfaces.FirstOrDefault(x => serviceType.GetInterfaces().Contains(x));

                if (interfaceType != null)
                {
                    serviceCollection.AddSingleton(interfaceType, serviceType);
                }
                else
                {
                    serviceCollection.AddSingleton(serviceType, serviceType);
                }
            }
        }
    }
}