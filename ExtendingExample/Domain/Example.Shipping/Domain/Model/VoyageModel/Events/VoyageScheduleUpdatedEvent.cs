using EventFlow.Aggregates;
using Example.Shipping.Domain.Model.VoyageModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.VoyageModel.Events
{
    public class VoyageScheduleUpdatedEvent : AggregateEvent<VoyageAggregate, VoyageId>
    {
        public VoyageScheduleUpdatedEvent(
            Schedule schedule)
        {
            Schedule = schedule;
        }

        public Schedule Schedule { get; }
    }
}
