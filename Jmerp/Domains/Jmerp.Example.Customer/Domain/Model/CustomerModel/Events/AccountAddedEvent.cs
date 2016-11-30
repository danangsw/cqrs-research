using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using System.Collections.Generic;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Events
{
    [EventVersion("AccountAdded", 1)]
    public class AccountAddedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public List<Account> Accounts { get; private set; }

        public AccountAddedEvent(List<Account> accounts)
        {
            Accounts = accounts;
        }
    }
}
