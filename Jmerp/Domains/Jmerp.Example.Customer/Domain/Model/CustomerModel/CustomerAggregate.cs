using EventFlow.Aggregates;
using EventFlow.Exceptions;
using EventFlow.Extensions;
using Jmerp.Commons;
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
        public CustomerAggregate(CustomerId id) : base(id)
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

        public void Apply(CustomerCreatedEvent e)
        {
            GeneralInfo = e.GeneralInfo;
        }
    }
}
