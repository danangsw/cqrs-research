using EventFlow.Aggregates;
using EventFlow.Jobs;
using EventFlow.Subscribers;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Jobs;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Domain.Model.CargoModel.Subscribers
{
     public class ScheduleChangedSubscriber :
         ISubscribeSynchronousTo<VoyageAggregate, VoyageId, VoyageScheduleUpdatedEvent>
     {
         private readonly IJobScheduler _jobScheduler;

         public ScheduleChangedSubscriber(
             IJobScheduler jobScheduler)
         {
             _jobScheduler = jobScheduler;
         }

         public Task HandleAsync(IDomainEvent<VoyageAggregate, VoyageId, VoyageScheduleUpdatedEvent> domainEvent, CancellationToken cancellationToken)
         {
             var job = new VerifyCargosForVoyageJob(
                 domainEvent.AggregateIdentity);
             return _jobScheduler.ScheduleNowAsync(job, cancellationToken);
         }
     }
}
