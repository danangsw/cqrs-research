using System;
using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Events;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;

namespace Jmerp.Example.Customers.Queries.InMemory.Customers
{
    public class CustomerReadModel : IReadModel
        ,IAmReadModelFor<CustomerAggregate, CustomerId, CustomerCreatedEvent>
        ,IAmReadModelFor<CustomerAggregate, CustomerId, GeneralInfoUpdatedEvent>
        ,IAmReadModelFor<CustomerAggregate, CustomerId, AddressAddedEvent>
        ,IAmReadModelFor<CustomerAggregate, CustomerId, AddressUpdatedEvent>
        ,IAmReadModelFor<CustomerAggregate,CustomerId, AddressAsDefaultSetEvent>
    {
        public CustomerId Id { get; private set; }
        public GeneralInfo GeneralInfo { get; private set; }

        public AddressDetail AddressDetail { get; private set; }

        public Customer toCustomer()
        {
            return new Customer(
                Id,
                GeneralInfo,
                AddressDetail);
        }

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
            Id = domainEvent.AggregateIdentity;
            AddressDetail = domainEvent.AggregateEvent.AddressDetail;
        }

        public void Apply(IReadModelContext context, IDomainEvent<CustomerAggregate, CustomerId, AddressUpdatedEvent> domainEvent)
        {
            AddressDetail = domainEvent.AggregateEvent.AddressDetail;
        }

        public void Apply(IReadModelContext context, IDomainEvent<CustomerAggregate, CustomerId, AddressAsDefaultSetEvent> domainEvent)
        {
            AddressDetail = domainEvent.AggregateEvent.AddressDetail;
        }
    }
}
