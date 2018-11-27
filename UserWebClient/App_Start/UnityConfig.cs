using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Unity.Injection;
using UserWebClient.Models;
using UserWebClient.RestaurantService;

namespace UserWebClient
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IRestaurantService, RestaurantServiceClient>(new InjectionConstructor());
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}