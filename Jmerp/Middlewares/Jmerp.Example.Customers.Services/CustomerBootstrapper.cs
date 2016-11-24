using EventFlow;
using Jmerp.Example.Customers.Middlewares.Mappings;
using Jmerp.Example.Customers.Middlewares.Services;
using Jmerp.Example.Customers.Queries.InMemory;

namespace Jmerp.Example.Customers.Middlewares
{
    public static class CustomerBootstrapper
    {
        //public static Assembly Assembly { get; } = typeof(CustomerConfiguration).Assembly;

        public static IEventFlowOptions CustomerBootstrapperConfiguration(this IEventFlowOptions options)
        {
            CustomerMapperConfiguration.Configure();

            return options
                //.AddDefaults(Assembly)
                .CustomerConfigurationDomain()
                .ConfigureCustomerQueriesInMemory()
                .RegisterServices(sr =>
                {
                    sr.Register<ICreateGeneralInfoApplicationServices, CreateGeneralInfoApplicationServices>();
                    sr.Register<IUpdateGeneralInfoApplicationServices, UpdateGeneralInfoApplicationServices>();
                });
        }
    }
}
