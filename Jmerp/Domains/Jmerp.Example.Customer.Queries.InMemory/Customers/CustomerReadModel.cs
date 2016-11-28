using System;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Events;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;

namespace Jmerp.Example.Customers.Queries.InMemory.Customers
{
    public class CustomerReadModel : IReadModel,
        IAmReadModelFor<CustomerAggregate, CustomerId, CustomerCreatedEvent>,
        IAmReadModelFor<CustomerAggregate, CustomerId, GeneralInfoUpdatedEvent>
    {
        public CustomerId Id { get; private set; }
        public GeneralInfo GeneralInfo { get; private set; }

        public AddressDetail AddressDetail { get; private set; }

        public void Apply(IReadModelContext context, IDomainEvent<CustomerAggregate, CustomerId, CustomerCreatedEvent> domainEvent)
        {
            Id = domainEvent.AggregateIdentity;
            GeneralInfo = domainEvent.AggregateEvent.GeneralInfo;
        }

        public void Apply(IReadModelContext context, IDomainEvent<CustomerAggregate, CustomerId, GeneralInfoUpdatedEvent> domainEvent)
        {
            GeneralInfo = domainEvent.AggregateEvent.GeneralInfo;
        }

        public void Apply(IReadModelContext context, IDomainEvent<CustomerAggregate, CustomerId, AddressAddedEvent> domainEvent)
        {
            AddressDetail = domainEvent.AggregateEvent.AddressDetail;
        }

        public Customer toCustomer()
        {
            return new Customer(
                Id,
                GeneralInfo,
                AddressDetail);
        }
    }
}
