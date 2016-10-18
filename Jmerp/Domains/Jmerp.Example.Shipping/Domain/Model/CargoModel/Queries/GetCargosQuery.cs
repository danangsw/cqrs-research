using EventFlow.Queries;
using System.Collections.Generic;
using System.Linq;

namespace Jmerp.Example.Shipping.Domain.Model.CargoModel.Queries
{
    public class GetCargosQuery : IQuery<IReadOnlyCollection<Cargo>>
    {
        public GetCargosQuery(
            params CargoId[] cargoIds)
            : this((IEnumerable<CargoId>)cargoIds)
        {
        }

        public GetCargosQuery(IEnumerable<CargoId> cargoIds)
        {
            CargoIds = cargoIds.ToList();
        }

        public IReadOnlyCollection<CargoId> CargoIds { get; }
    }
}
