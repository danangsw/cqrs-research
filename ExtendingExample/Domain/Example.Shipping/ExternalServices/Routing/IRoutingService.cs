using Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Shipping.ExternalServices.Routing
{
    public interface IRoutingService
    {
        Task<IReadOnlyCollection<Itinerary>> CalculateItinerariesAsync(Route route, CancellationToken cancellationToken);
    }
}
