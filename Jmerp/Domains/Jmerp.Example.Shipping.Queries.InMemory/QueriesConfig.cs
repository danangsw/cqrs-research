using EventFlow;
using EventFlow.Extensions;
using Jmerp.Example.Shipping.Queries.InMemory.Cargos;
using Jmerp.Example.Shipping.Queries.InMemory.Voyages;
using System.Reflection;


namespace Jmerp.Example.Shipping.Queries.InMemory
{
    public static class QueriesConfig
    {
        public static Assembly Assembly { get; } = typeof(QueriesConfig).Assembly;

        public static IEventFlowOptions ConfigureShippingQueriesInMemory(
            this IEventFlowOptions eventFlowOptions)
        {
            return eventFlowOptions
                .AddQueryHandlers(Assembly)
                .UseInMemoryReadStoreFor<VoyageReadModel>()
                .UseInMemoryReadStoreFor<CargoReadModel>();
        }
    }
}
