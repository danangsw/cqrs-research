using Example.Shipping.Domain.Model.CargoModel;
using Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Shipping.Application
{
    public interface IBookingApplicationService
    {
        Task<CargoId> BookCargoAsync(Route route, CancellationToken cancellationToken);
    }
}
