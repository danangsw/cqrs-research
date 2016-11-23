using Autofac;
using EventFlow;
using EventFlow.Autofac.Extensions;
using Jmerp.Example.Customers.Middlewares;

namespace Jmerp.Middlewares.Boostrappers
{
    public static class MiddlewaresBootrappers
    {
        public static IContainer MiddlewaresStartUp(this ContainerBuilder containerBuilder)
        {
            var container = EventFlowOptions.New
                           .UseAutofacContainerBuilder(containerBuilder) // Must be the first line!
                           .CustomerBootstrapperConfiguration()
                           .CreateContainer();

            return container;
        }
    }
}
