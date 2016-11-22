using EventFlow.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventFlow.Configuration;
using System.Threading;
using EventFlow;
using Jmerp.Commons;
using Jmerp.Example.Customers.Domain.Model.CustomerModel.Commands;

namespace Jmerp.Example.Customers.Domain.Model.CustomerModel.Jobs
{
    public class VerifyCustomerDomainErrorJob : IJob
    {
        public VerifyCustomerDomainErrorJob(
            CustomerId customerId,
            SubscriberResult subscriberResult)
        {
            SubscriberResult = subscriberResult;
            CustomerId = customerId;
        }
        public SubscriberResult SubscriberResult { get; private set; }
        public CustomerId CustomerId { get; }

        public async Task ExecuteAsync(IResolver resolver, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();

            await commandBus.PublishAsync(new CustomerDomainErrorCommand(CustomerId, SubscriberResult), cancellationToken).ConfigureAwait(false);

            return;
        }
    }
}
