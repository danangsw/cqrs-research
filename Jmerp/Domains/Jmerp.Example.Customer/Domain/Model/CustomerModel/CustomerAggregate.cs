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
        public SubscriberResult SubscriberResult { get; private set; }
        public CustomerAggregate(CustomerId id) : base(id)
        {
        }

        public void CustomerDomainError(
            SubscriberResult subscriberResult)
        {
            try
            {
                if (SubscriberResult != null)
                {
                    throw DomainError.With("---CustomerDomainError already received---");
                }

                Emit(new CustomerDomainErrorReceivedEvent(subscriberResult));
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Create(
            GeneralInfo generalInfo)
        {
            try
            {
                // If the aggregate isn't new, i.e., events have already
                // been fired for this aggregate, then we have a domain error
                Specs.AggregateIsNew.ThrowDomainErrorIfNotStatisfied(this);

                // Everything is okay and thus we emit the event
                Emit(new CustomerCreatedEvent(generalInfo));
            }
            catch (Exception e)
            {
                Emit(new CustomerDomainRespondedEvent(new Commons.SubscriberResult(true, new List<string> { "Subscriber got CustomerDomainErrorEvent" })));
            }
        }

        public void Apply(CustomerCreatedEvent e)
        {
            GeneralInfo = e.GeneralInfo;
        }

        public void Apply(CustomerDomainRespondedEvent e)
        {
            SubscriberResult = e.Result;
        }

        public void Apply(CustomerDomainErrorReceivedEvent e)
        {
            SubscriberResult = e.SubscriberResult;
        }
    }
}
