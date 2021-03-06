﻿using EventFlow.Queries;
using Example.Shipping.Domain.Model.CargoModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.CargoModel.Queries
{
    public class DeleteTransportLegQuery : IQuery<int>
    {
        public DeleteTransportLegQuery(
            TransportLegId transportLegId
            )
        {
            TransportLegId = transportLegId;
        }

        public TransportLegId TransportLegId { get; }

    }
}
