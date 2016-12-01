using Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Services
{
    public interface IUpdateItineraryService
    {
        Task<Itinerary> UpdateItineraryAsync(Itinerary itinerary, CancellationToken cancellationToken);
    }
}
