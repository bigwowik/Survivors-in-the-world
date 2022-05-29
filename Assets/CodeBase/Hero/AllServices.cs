using System;
using System.Collections.Generic;

namespace CodeBase.Hero
{
    public class AllServices
    {
        public static AllServices instance;
        public static AllServices Container => 
            instance ??= new AllServices();

        public static Dictionary<Type, IService> services = new Dictionary<Type, IService>();

        
        public void Register<TService>(TService service) where TService : IService
        {
            services[typeof(TService)] = service;
        }
        
        public TService Single<TService>() where TService : IService
        {
            return (TService) services[typeof(TService)];
        }
    }
}