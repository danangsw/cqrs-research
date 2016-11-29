using EventFlow.Aggregates;
using EventFlow.Exceptions;
using EventFlow.Extensions;
using Jmerp.Commons;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Entities;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Events;
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

        public CustomerAggregate(CustomerId id) : base(id)
        {
        }

        public void SetAddressDetail(
            List<Address> addresses)
        {
            var addressDetailNew = new AddressDetail(addresses);
            Emit(new AddressDetailSetEvent(addressDetailNew));
        }

        public void SetAsDefaultShipping(
            AddressId addressId)
        {

        }

        public void SetAsDefaultMailing(
            AddressId addressId)
        {

        }

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
                addressDetailNew = AddressDetail.Add(addresses);
            }

            Emit(new AddressAddedEvent(addressDetailNew));
        }

        public void Apply(AddressAddedEvent e)
        {
            Specs.AggregateIsCreated.ThrowDomainErrorIfNotStatisfied(this);

            AddressDetail = e.AddressDetail;
        }
        #endregion
    }
}
