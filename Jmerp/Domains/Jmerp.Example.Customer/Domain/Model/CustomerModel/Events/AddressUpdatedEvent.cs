using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using System.Collections.Generic;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Events
{
    [EventVersion("AddressUpdated", 1)]
    public class AddressUpdatedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public List<Address> Address { get; }

        public AddressUpdatedEvent(List<Address> address)
        {
            Address = address;
        }
    }
}
