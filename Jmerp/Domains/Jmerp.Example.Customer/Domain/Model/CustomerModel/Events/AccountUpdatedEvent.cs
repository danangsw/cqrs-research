using EventFlow.Aggregates;
using EventFlow.EventStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Events
{
    [EventVersion("AccountUpdated", 1)]
    public class AccountUpdatedEvent : AggregateEvent<CustomerAggregate, CustomerId>
    {
        public Account Account { get; private set; }

        public AccountUpdatedEvent(Account account)
        {
            Account = account;
        }
    }
}
