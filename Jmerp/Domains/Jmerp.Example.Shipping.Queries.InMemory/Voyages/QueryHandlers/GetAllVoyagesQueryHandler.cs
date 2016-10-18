using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Queries.InMemory.Voyages.QueryHandlers
{
    public class GetAllVoyagesQueryHandler : IQueryHandler<GetAllVoyagesQuery, IReadOnlyCollection<Voyage>>
    {
        private readonly IInMemoryReadStore<VoyageReadModel> _readStore;

        public GetAllVoyagesQueryHandler(
            IInMemoryReadStore<VoyageReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<IReadOnlyCollection<Voyage>> ExecuteQueryAsync(GetAllVoyagesQuery query, CancellationToken cancellationToken)
        {
            var voyageReadModels = await _readStore.FindAsync(rm => true, cancellationToken).ConfigureAwait(false);
            return voyageReadModels.Select(rm => rm.ToVoyage()).ToList();
        }
    }
}
