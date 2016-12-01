using EventFlow.Queries;
using Example.Shipping.Domain.Model.CargoModel.Entities;
using Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using Example.Shipping.Domain.Model.VoyageModel.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Services
{
    public class UpdateItineraryService : IUpdateItineraryService
    {
        private readonly IQueryProcessor _queryProcessor;

        public UpdateItineraryService(
            IQueryProcessor queryProcessor)
        {
            _queryProcessor = queryProcessor;
        }

        public async Task<Itinerary> UpdateItineraryAsync(Itinerary itinerary, CancellationToken cancellationToken)
        {
            var voyageIds = itinerary.TransportLegs
                .Select(l => l.VoyageId)
                .Distinct();

            var voyages = await _queryProcessor.ProcessAsync(new GetVoyagesQuery(voyageIds), cancellationToken).ConfigureAwait(false);

            var carrierMovements = voyages
                .SelectMany(v => v.Schedule.CarrierMovements.Select(cm => new { VoyageId = v.Id, CarrierMovement = cm }))
                .ToDictionary(a => a.CarrierMovement.Id, a => a);

            var transportLegs = itinerary.TransportLegs.Select(l =>
            {
                var a = carrierMovements[l.CarrierMovementId];
                var cm = a.CarrierMovement;
                return new TransportLeg(
                    l.Id,
                    cm.DepartureLocationId,
                    cm.ArrivalLocationId,
                    cm.DepartureTime,
                    cm.ArrivalTime,
                    a.VoyageId,
                    l.CarrierMovementId);
            });

            return new Itinerary(transportLegs);
        }
    }
}
