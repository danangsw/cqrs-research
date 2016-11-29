using System;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Events;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;

namespace Jmerp.Example.Customers.Queries.InMemory.Customers
{
    public class CustomerAddressReadModel : IReadModel,
         IAmReadModelFor<CustomerAggregate, CustomerId, AddressAddedEvent>
    {
        public void Apply(IReadModelContext context, IDomainEvent<CustomerAggregate, CustomerId, AddressAddedEvent> domainEvent)
        {
        }
    }
}
