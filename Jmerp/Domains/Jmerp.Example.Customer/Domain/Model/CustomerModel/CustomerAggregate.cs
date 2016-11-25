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
            AddressDetail addressDetail)
        {
            Emit(new AddressDetailSetEvent(addressDetail));
        }

        public void AddAddress(
            Address address)
        {
            
        }

        public void UpdateAddress(
            Address address)
        {
           
        }

        public void SetAsDefaultShipping(
            AddressId addressId)
        {

        }

        public void SetAsDefaultMailing(
            AddressId addressId)
        {

        }

        public void Create(
            GeneralInfo generalInfo)
        {
            // If the aggregate isn't new, i.e., events have already
            // been fired for this aggregate, then we have a domain error
            Specs.AggregateIsNew.ThrowDomainErrorIfNotStatisfied(this);

            // Everything is okay and thus we emit the event
            Emit(new CustomerCreatedEvent(generalInfo));
        }

        public void Update(
            string organizationName,
            string contactPerson,
            string phone,
            string fax,
            string email,
            string web)
        {
            var generalInfo = new GeneralInfo(organizationName,contactPerson,phone,fax,email,web);

            Emit(new GeneralInfoUpdatedEvent(generalInfo));
        }

        public void Apply(CustomerCreatedEvent e)
        {
            GeneralInfo = e.GeneralInfo;
        }

        public void Apply(GeneralInfoUpdatedEvent e)
        {
            Specs.AggregateIsCreated.ThrowDomainErrorIfNotStatisfied(this);

            GeneralInfo = e.GeneralInfo;
        }
    }
}
