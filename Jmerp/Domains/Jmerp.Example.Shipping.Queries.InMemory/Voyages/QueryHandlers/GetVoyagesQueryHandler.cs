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
    public class GetVoyagesQueryHandler : IQueryHandler<GetVoyagesQuery, IReadOnlyCollection<Voyage>>
    {
        private readonly IInMemoryReadStore<VoyageReadModel> _readStore;

        public GetVoyagesQueryHandler(
            IInMemoryReadStore<VoyageReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<IReadOnlyCollection<Voyage>> ExecuteQueryAsync(GetVoyagesQuery query, CancellationToken cancellationToken)
        {
            var voyageIds = new HashSet<VoyageId>(query.VoyageIds);
            var voyageReadModels = await _readStore.FindAsync(rm => voyageIds.Contains(rm.Id), cancellationToken).ConfigureAwait(false);
            return voyageReadModels.Select(rm => rm.ToVoyage()).ToList();
        }
    }
}
