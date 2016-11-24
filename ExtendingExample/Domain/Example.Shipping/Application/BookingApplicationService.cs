using EventFlow;
using EventFlow.Exceptions;
using Example.Shipping.Domain.Model.CargoModel;
using Example.Shipping.Domain.Model.CargoModel.Commands;
using Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using Example.Shipping.ExternalServices.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Shipping.Application
{
    public class BookingApplicationService : IBookingApplicationService
    {
        private readonly ICommandBus _commandBus;
        private readonly IRoutingService _routingService;

        public BookingApplicationService(
            ICommandBus commandBus,
            IRoutingService routingService)
        {
            _commandBus = commandBus;
            _routingService = routingService;
        }

        public async Task<CargoId> BookCargoAsync(Route route, CancellationToken cancellationToken)
        {
            var cargoId = CargoId.New;
            await _commandBus.PublishAsync(new CargoBookCommand(cargoId, route), cancellationToken).ConfigureAwait(false);

            var itineraries = await _routingService.CalculateItinerariesAsync(route, cancellationToken).ConfigureAwait(false);

            var itinerary = itineraries.FirstOrDefault();
            if (itinerary == null)
            {
                throw DomainError.With("Could not find itinerary");
            }

            await _commandBus.PublishAsync(new CargoSetItineraryCommand(cargoId, itinerary), cancellationToken).ConfigureAwait(false);

            return cargoId;
        }
    }
}
