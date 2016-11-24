using EventFlow;
using EventFlow.Extensions;
using Jmerp.Example.Customers.Middlewares.Mappings;
using Jmerp.Example.Customers.Middlewares.Services;
using Jmerp.Example.Customers.Queries.InMemory;
using System.Reflection;

namespace Jmerp.Example.Customers.Middlewares
{
    public static class CustomerBootstrapper
    {
        //public static Assembly Assembly { get; } = typeof(CustomerConfiguration).Assembly;

        public static IEventFlowOptions CustomerBootstrapperConfiguration(this IEventFlowOptions options)
        {
            MapperConfiguration.Configure();

            return options
                //.AddDefaults(Assembly)
                .CustomerConfigurationDomain()
                .ConfigureCustomerQueriesInMemory()
                .RegisterServices(sr =>
                {
                    sr.Register<ICreateGeneralInfoApplicationServices, CreateGeneralInfoApplicationServices>();
                });
        }
    }
}
