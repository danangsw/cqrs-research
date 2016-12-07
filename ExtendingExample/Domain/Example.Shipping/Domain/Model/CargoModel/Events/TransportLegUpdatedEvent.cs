﻿
using EventFlow.Aggregates;
using EventFlow.EventStores;
using Example.Shipping.Domain.Model.CargoModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.CargoModel.Events
{
    [EventVersion("TransportLegUpdated", 1)]
    public class TransportLegUpdatedEvent : AggregateEvent<CargoAggregate, CargoId>
    {
        public TransportLegUpdatedEvent(
            TransportLeg transportLeg
        )
        {
            TransportLeg = transportLeg;
        }

        public TransportLeg TransportLeg { get; }
    }
}
