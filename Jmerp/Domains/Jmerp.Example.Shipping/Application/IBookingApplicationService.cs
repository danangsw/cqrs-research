using Jmerp.Example.Shipping.Domain.Model.CargoModel;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using System.Threading;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Application
{
    public interface IBookingApplicationService
    {
        Task<CargoId> BookCargoAsync(Route route, CancellationToken cancellationToken);
    }
}
