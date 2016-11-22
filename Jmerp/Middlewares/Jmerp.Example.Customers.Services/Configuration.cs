using EventFlow;
using EventFlow.Extensions;
using Jmerp.Example.Customers.Middlewares.Applications;
using Jmerp.Example.Customers.Middlewares.Mappings;
using System.Reflection;

namespace Jmerp.Example.Customers.Middlewares
{
    public static class Configuration
    {
        public static Assembly Assembly { get; } = typeof(Configuration).Assembly;

        public static IEventFlowOptions ConfigureCustomerServices(this IEventFlowOptions options)
        {
            MapperConfiguration.Configure();

            return options
                 .AddDefaults(Assembly)
                 .RegisterServices( sr =>
                 {
                     sr.Register<IGeneralInfoApplicationServices, GeneralInfoApplicationServices>();
                 });
        }
    }
}
