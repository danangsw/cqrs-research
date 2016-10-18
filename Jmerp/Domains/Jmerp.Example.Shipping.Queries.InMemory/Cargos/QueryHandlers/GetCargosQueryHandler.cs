using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using Jmerp.Example.Shipping.Domain.Model.CargoModel;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Queries.InMemory.Cargos.QueryHandlers
{
    public class GetCargosQueryHandler : IQueryHandler<GetCargosQuery, IReadOnlyCollection<Cargo>>
    {
        private readonly IInMemoryReadStore<CargoReadModel> _readStore;

        public GetCargosQueryHandler(
            IInMemoryReadStore<CargoReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<IReadOnlyCollection<Cargo>> ExecuteQueryAsync(GetCargosQuery query, CancellationToken cancellationToken)
        {
            var cargoIds = new HashSet<CargoId>(query.CargoIds);
            var cargoReadModels = await _readStore.FindAsync(
                rm => cargoIds.Contains(rm.Id),
                cancellationToken)
                .ConfigureAwait(false);
            return cargoReadModels.Select(rm => rm.ToCargo()).ToList();
        }
    }
}
