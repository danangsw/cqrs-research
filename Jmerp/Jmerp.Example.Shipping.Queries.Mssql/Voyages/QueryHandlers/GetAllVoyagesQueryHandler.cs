using EventFlow.MsSql.ReadStores;
using EventFlow.Queries;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Queries.Mssql.Voyages.QueryHandlers
{
    public class GetAllVoyagesQueryHandler : IQueryHandler<GetAllVoyagesQuery, IReadOnlyCollection<Voyage>>
    {
        private readonly IMssqlReadModelStore<VoyageReadModel> _readStore;

        public GetAllVoyagesQueryHandler(
            IMssqlReadModelStore<VoyageReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<IReadOnlyCollection<Voyage>> ExecuteQueryAsync(GetAllVoyagesQuery query, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
