using Autofac;
using EventFlow;
using EventFlow.Autofac.Extensions;
using Jmerp.Commons;
using Jmerp.Example.Customers.Middlewares.Mappings;
using Jmerp.Example.Customers.Middlewares.Services;
using Jmerp.Example.Customers.Queries.InMemory;

namespace Jmerp.Example.Customers.Middlewares
{
    public class CustomerMiddlewaresBootstrapper : Bootstrapper
    {
        public static void Build()
        {
            MapperConfiguration.Configure();

            var containerBuilder = new ContainerBuilder();

            var container = EventFlowOptions.New
                            .UseAutofacContainerBuilder(containerBuilder) // Must be the first line!
                            .CustomerConfigurationDomain()
                            .ConfigureCustomerQueriesInMemory()
                            .RegisterServices(sr =>
                            {
                                sr.Register<ICreateGeneralInfoApplicationServices, CreateGeneralInfoApplicationServices>();
                            })
                            .CreateContainer();

           

            SetAutofacContainer(container);
        }
    }
}
