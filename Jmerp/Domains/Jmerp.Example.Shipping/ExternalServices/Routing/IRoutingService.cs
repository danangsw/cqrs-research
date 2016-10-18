using Jmerp.Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.ExternalServices.Routing
{
    public interface IRoutingService
    {
        Task<IReadOnlyCollection<Itinerary>> CalculateItinerariesAsync(Route route, CancellationToken cancellationToken);
    }
}
