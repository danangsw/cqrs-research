using EventFlow;
using EventFlow.Extensions;
using Jmerp.Example.Customers.Queries.InMemory.Customers;
using Jmerp.Example.Customers.Queries.InMemory.Customers.ReadModelLocators;
using System.Reflection;

namespace Jmerp.Example.Customers.Queries.InMemory
{
    public static class QueriesConfig
    {
        public static Assembly Assembly { get; } = typeof(QueriesConfig).Assembly;

        public static IEventFlowOptions ConfigureCustomerQueriesInMemory(
            this IEventFlowOptions options)
        {
            return options
                .AddQueryHandlers(Assembly)
                .UseInMemoryReadStoreFor<CustomerReadModel>();
                //.UseInMemoryReadStoreFor<CustomerReadModel, CustomerAddressReadModelLocator>();
        }
    }
}
