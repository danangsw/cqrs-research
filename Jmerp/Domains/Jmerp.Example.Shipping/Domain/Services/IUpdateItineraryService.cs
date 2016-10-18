using Jmerp.Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Domain.Services
{
    public interface IUpdateItineraryService
    {
        Task<Itinerary> UpdateItineraryAsync(Itinerary itinerary, CancellationToken cancellationToken);
    }
}
