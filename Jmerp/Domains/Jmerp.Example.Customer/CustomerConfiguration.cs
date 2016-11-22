using EventFlow;
using EventFlow.Extensions;
using Jmerp.Example.Customers.Application;
using System.Reflection;

namespace Jmerp.Example.Customers
{
    public static class CustomerConfiguration
    {
        public static Assembly Assembly { get; } = typeof(CustomerConfiguration).Assembly;

        public static IEventFlowOptions CustomerConfigurationDomain(this IEventFlowOptions options)
        {
            return options
                .AddDefaults(Assembly)
                .RegisterServices(sr => 
                {
                   sr.Register<ICustomerApplicationService, CustomerApplicationService>();                 
                })
                ;
        }
    }
}
