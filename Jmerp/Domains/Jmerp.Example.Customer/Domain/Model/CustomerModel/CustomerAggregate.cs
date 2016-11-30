using EventFlow.Aggregates;
using EventFlow.Exceptions;
using EventFlow.Extensions;
using Jmerp.Commons;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Events;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Helpers;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Specifications;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.ValueObjects;
using Jmerp.Example.Shipping.Domain;
using System;
using System.Collections.Generic;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel
{
    public class CustomerAggregate : AggregateRoot<CustomerAggregate, CustomerId>,
        IEmit<CustomerCreatedEvent>
    {
        public GeneralInfo GeneralInfo { get; private set; }
        public AddressDetail AddressDetail { get; private set; }
        public AccountingDetail AccountingDetail { get; private set; }

        public CustomerAggregate(CustomerId id) : base(id)
        {
        }

        #region Accounts Added
        public void AddAccounts(List<Account> accounts)
        {
            Emit(new AccountAddedEvent(accounts));
        }

        public void Apply(AccountAddedEvent e)
        {
            Specs.AggregateIsCreated.ThrowDomainErrorIfNotStatisfied(this);
            var accounts = AccountingDetail.AddAccount(e.Accounts);
            AccountingDetail = accounts;
        }
        #endregion

        #region Account Updated
        public void UpdateAccount(Account account)
        {
            Emit(new AccountUpdatedEvent(account));
        }

        public void Apply(AccountUpdatedEvent e)
        {
            Specs.AggregateIsCreated.ThrowDomainErrorIfNotStatisfied(this);
            var accounts = AccountingDetail.UpdateAccount(e.Account);
            AccountingDetail = accounts;
        }
        #endregion

        #region Accounts Removed
        public void RemoveAccounts(List<AccountId> accountIds)
        {
            Emit(new AccountRemovedEvent(accountIds));
        }

        public void Apply(AccountRemovedEvent e)
        {
            Specs.AggregateIsCreated.ThrowDomainErrorIfNotStatisfied(this);
            var accounts = AccountingDetail.RemoveAccount(e.AccountIds);
            AccountingDetail = accounts;
        }
        #endregion

        #region Address Set as Default
        public void SetAsDefault(
            AddressId addressId, string addressType)
        {
            if (addressType == CustomerAddressTypeConstants.BillingAddress)
            {
                var updatedAddressDetail = AddressDetail.SetAsDefaultBillingAddress(addressId);
                Emit(new AddressAsBillingDefaultUpdatedEvent(updatedAddressDetail));
            }
            else if (addressType == CustomerAddressTypeConstants.ShippingAddress)
            {
                var updatedAddressDetail = AddressDetail.SetAsDefaultShippingAddress(addressId);
                Emit(new AddressAsShippingDefaultUpdatedEvent(updatedAddressDetail));
            }
        }

        public void Apply(AddressAsBillingDefaultUpdatedEvent e)
        {
            Specs.AggregateIsCreated.ThrowDomainErrorIfNotStatisfied(this);

            AddressDetail = e.AddressDetail;
        }

        public void Apply(AddressAsShippingDefaultUpdatedEvent e)
        {
            Specs.AggregateIsCreated.ThrowDomainErrorIfNotStatisfied(this);

            AddressDetail = e.AddressDetail;
        }

        #endregion

        #region Customer Created
        public void Create(
            GeneralInfo generalInfo)
        {
            // If the aggregate isn't new, i.e., events have already
            // been fired for this aggregate, then we have a domain error
            Specs.AggregateIsNew.ThrowDomainErrorIfNotStatisfied(this);

            // Everything is okay and thus we emit the event
            Emit(new CustomerCreatedEvent(generalInfo));
        }

        public void Apply(CustomerCreatedEvent e)
        {
            GeneralInfo = e.GeneralInfo;
        }
        #endregion

        #region General Info Updated
        public void Update(
            string organizationName,
            string contactPerson,
            string phone,
            string fax,
            string email,
            string web)
        {
            var generalInfo = new GeneralInfo(organizationName, contactPerson, phone, fax, email, web);

            Emit(new GeneralInfoUpdatedEvent(generalInfo));
        }

        public void Apply(GeneralInfoUpdatedEvent e)
        {
            Specs.AggregateIsCreated.ThrowDomainErrorIfNotStatisfied(this);

            GeneralInfo = e.GeneralInfo;
        }
        #endregion

        #region Address Detail Updated
        public void UpdateAddress(Address address)
        {
            var updatedAddressDetail = AddressDetail.UpdateAddress(address);

            Emit(new AddressUpdatedEvent(updatedAddressDetail));
        }

        public void Apply(AddressUpdatedEvent e)
        {
            Specs.AggregateIsCreated.ThrowDomainErrorIfNotStatisfied(this);

            AddressDetail = e.AddressDetail;
        }
        #endregion

        #region Address Detail Added
        public void AddAddress(
            List<Address> addresses)
        {
            var addressDetailNew = new AddressDetail(addresses);

            if (AddressDetail != null)
            {
                addressDetailNew = AddressDetail.AddAddress(addresses);
            }

            Emit(new AddressAddedEvent(addressDetailNew));
        }

        public void Apply(AddressAddedEvent e)
        {
            Specs.AggregateIsCreated.ThrowDomainErrorIfNotStatisfied(this);

            AddressDetail = e.AddressDetail;
        }
        #endregion

        #region Address Detail Removed
        public void RemoveAddress(
            AddressId addressId)
        {
            var addressDetailRemove = AddressDetail.RemoveAddress(addressId);

            Emit(new AddressRemovedEvent(addressDetailRemove));
        }

        public void Apply(AddressRemovedEvent e)
        {
            Specs.AggregateIsCreated.ThrowDomainErrorIfNotStatisfied(this);

            AddressDetail = e.AddressDetail;
        }
        #endregion
    }
}
