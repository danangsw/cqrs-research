using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using System.Collections.Generic;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Events
{
    [EventVersion("AccountRemoved", 1)]
    public class AccountRemovedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public List<AccountId> AccountIds { get; private set; }

        public AccountRemovedEvent(List<AccountId> accountIds)
        {
            AccountIds = accountIds;
        }
    }
}
