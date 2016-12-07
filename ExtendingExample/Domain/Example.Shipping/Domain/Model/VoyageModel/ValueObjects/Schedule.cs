using EventFlow.ValueObjects;
using Example.General.Extension;
using Example.Shipping.Domain.Model.VoyageModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.VoyageModel.ValueObjects
{
    public class Schedule : ValueObject
    {
        
        public Schedule() {
            CarrierMovements = new List<CarrierMovement>();
        }

        public Schedule(
            IEnumerable<CarrierMovement> carrierMovements)
        {
            var carrierMovementList = (carrierMovements ?? Enumerable.Empty<CarrierMovement>()).ToList();

            if (!carrierMovementList.Any()) throw new ArgumentException(nameof(carrierMovements));

            CarrierMovements = carrierMovementList;
        }

        public IReadOnlyList<CarrierMovement> CarrierMovements { get; }

        public Schedule Delay(TimeSpan delay)
        {
            var carrierMovements = CarrierMovements
                .Select(m => new CarrierMovement(
                    m.Id,
                    m.DepartureLocationId,
                    m.ArrivalLocationId,
                    m.DepartureTime + delay,
                    m.ArrivalTime + delay));
            return new Schedule(carrierMovements);
        }


        public Schedule AddCarrierMovement(CarrierMovement carrierMovement)
        {
            var newListCarrierMovement = CarrierMovements.AddList<CarrierMovement, CarrierMovementId>(carrierMovement);
            return new Schedule(newListCarrierMovement);
        }

        public Schedule UpdateCarrierMovement(CarrierMovement carrierMovement)
        {
            var newListCarrierMovement = CarrierMovements.UpdateList<CarrierMovement, CarrierMovementId>(carrierMovement);
            return new Schedule(newListCarrierMovement);
        }

        

    }
}
