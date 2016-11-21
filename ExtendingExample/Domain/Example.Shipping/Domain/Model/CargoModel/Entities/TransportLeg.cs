using EventFlow.Entities;
using Example.Shipping.Domain.Model.LocationModel;
using Example.Shipping.Domain.Model.VoyageModel;
using Example.Shipping.Domain.Model.VoyageModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.CargoModel.Entities
{
    public class TransportLeg : Entity<TransportLegId>
    {
        public TransportLeg(
            TransportLegId id,
            LocationId loadLocation,
            LocationId unloadLocation,
            DateTimeOffset loadTime,
            DateTimeOffset unloadTime,
            VoyageId voyageId,
            CarrierMovementId carrierMovementId)
            : base(id)
        {
            if (loadLocation == null) throw new ArgumentNullException(nameof(loadLocation));
            if (unloadLocation == null) throw new ArgumentNullException(nameof(unloadLocation));
            if (loadTime == default(DateTimeOffset)) throw new ArgumentOutOfRangeException(nameof(loadTime));
            if (unloadTime == default(DateTimeOffset)) throw new ArgumentOutOfRangeException(nameof(unloadTime));
            if (voyageId == null) throw new ArgumentNullException(nameof(voyageId));
            if (carrierMovementId == null) throw new ArgumentNullException(nameof(carrierMovementId));

            LoadLocation = loadLocation;
            UnloadLocation = unloadLocation;
            LoadTime = loadTime;
            UnloadTime = unloadTime;
            VoyageId = voyageId;
            CarrierMovementId = carrierMovementId;
        }

        public LocationId LoadLocation { get; }
        public LocationId UnloadLocation { get; }
        public DateTimeOffset LoadTime { get; }
        public DateTimeOffset UnloadTime { get; }
        public VoyageId VoyageId { get; }
        public CarrierMovementId CarrierMovementId { get; } // TODO: Do we really want this?
    }
}
