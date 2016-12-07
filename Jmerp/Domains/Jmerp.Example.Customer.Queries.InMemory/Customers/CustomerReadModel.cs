using EventFlow.Aggregates;
using EventFlow.ReadStores;
using Jmerp.Example.Customers.Domain.Model.CustomerModel;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Events;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;

namespace Jmerp.Example.Customers.Queries.InMemory.Customers
{
    public class CustomerReadModel : IReadModel
        , IAmReadModelFor<CustomerAggregate, CustomerId, CustomerCreatedEvent>
        , IAmReadModelFor<CustomerAggregate, CustomerId, GeneralInfoUpdatedEvent>
        , IAmReadModelFor<CustomerAggregate, CustomerId, AddressAddedEvent>
        , IAmReadModelFor<CustomerAggregate, CustomerId, AddressUpdatedEvent>
        , IAmReadModelFor<CustomerAggregate, CustomerId, AddressRemovedEvent>
        , IAmReadModelFor<CustomerAggregate,CustomerId, AddressAsShippingDefaultUpdatedEvent>
        , IAmReadModelFor<CustomerAggregate, CustomerId, AddressAsBillingDefaultUpdatedEvent>
        , IAmReadModelFor<CustomerAggregate, CustomerId, AccountAddedEvent>
        , IAmReadModelFor<CustomerAggregate, CustomerId, AccountUpdatedEvent>
        , IAmReadModelFor<CustomerAggregate, CustomerId, AccountRemovedEvent>
    {
        public CustomerId Id { get; private set; }
        public GeneralInfo GeneralInfo { get; private set; }

        public AddressDetail AddressDetail { get; private set; }
        public AccountingDetail AccountingDetail { get; private set; }

        public Customer toCustomer()
        {
            return new Customer(
                Id,
                GeneralInfo,
                AddressDetail,
                AccountingDetail);
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

        public void Apply(IReadModelContext context, IDomainEvent<CustomerAggregate, CustomerId, AddressAsShippingDefaultUpdatedEvent> domainEvent)
        {
            AddressDetail = domainEvent.AggregateEvent.AddressDetail;
        }

        public void Apply(IReadModelContext context, IDomainEvent<CustomerAggregate, CustomerId, AddressAsBillingDefaultUpdatedEvent> domainEvent)
        {
            AddressDetail = domainEvent.AggregateEvent.AddressDetail;
        }

        public void Apply(IReadModelContext context, IDomainEvent<CustomerAggregate, CustomerId, AddressRemovedEvent> domainEvent)
        {
            AddressDetail = domainEvent.AggregateEvent.AddressDetail;
        }

        public void Apply(IReadModelContext context, IDomainEvent<CustomerAggregate, CustomerId, AccountAddedEvent> domainEvent)
        {
            AddAccounts(domainEvent);
        }

        private void AddAccounts(IDomainEvent<CustomerAggregate, CustomerId, AccountAddedEvent> domainEvent)
        {
            if (AccountingDetail == null)
            {
                var accounts = new AccountingDetail(domainEvent.AggregateEvent.Accounts);
                AccountingDetail = accounts;
            }
            else
            {
                var accounts = AccountingDetail.AddAccount(domainEvent.AggregateEvent.Accounts);
                AccountingDetail = accounts;
            }
        }

        public void Apply(IReadModelContext context, IDomainEvent<CustomerAggregate, CustomerId, AccountUpdatedEvent> domainEvent)
        {
            var accounts = AccountingDetail.UpdateAccount(domainEvent.AggregateEvent.Account);
            AccountingDetail = accounts;
        }

        public void Apply(IReadModelContext context, IDomainEvent<CustomerAggregate, CustomerId, AccountRemovedEvent> domainEvent)
        {
            var accounts = AccountingDetail.RemoveAccount(domainEvent.AggregateEvent.AccountIds);
            AccountingDetail = accounts;
        }
    }
}
