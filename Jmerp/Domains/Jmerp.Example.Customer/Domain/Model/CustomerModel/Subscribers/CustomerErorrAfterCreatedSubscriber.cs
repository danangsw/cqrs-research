using EventFlow.Subscribers;
using System;
using System.Threading.Tasks;
using System.Threading;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Events;
using System.Collections.Generic;
using EventFlow.Jobs;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Jobs;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Subscribers
{
    public class CustomerErorrAfterCreatedSubscriber : ISubscribeSynchronousTo<CustomerAggregate, CustomerId, CustomerErorrAfterCreatedEvent>
    {
        private readonly IJobScheduler _jobScheduler;

        public CustomerErorrAfterCreatedSubscriber(
            IJobScheduler jobScheduler)
        {
            _jobScheduler = jobScheduler;
        }

        public Task HandleAsync(EventFlow.Aggregates.IDomainEvent<CustomerAggregate, CustomerId, CustomerErorrAfterCreatedEvent> domainEvent, CancellationToken cancellationToken)
        {
            Console.WriteLine("--Subscriber got CustomerDomainErrorEvent--");
            var job = new VerifyCustomerDomainErrorJob(domainEvent.AggregateIdentity, new Commons.SubscriberResult(true, new List<string> { "Subscriber got CustomerDomainErrorEvent" }));
            return _jobScheduler.ScheduleNowAsync(job, cancellationToken);
        }
    }
}
