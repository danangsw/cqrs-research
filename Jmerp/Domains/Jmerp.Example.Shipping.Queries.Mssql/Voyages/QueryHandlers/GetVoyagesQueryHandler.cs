using EventFlow.MsSql;
using EventFlow.MsSql.ReadStores;
using EventFlow.Queries;
using Jmerp.Commons.Extension;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Queries;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.ValueObjects;
using Jmerp.Example.Shipping.Queries.Mssql.Voyages.QueriesSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Queries.Mssql.Voyages.QueryHandlers
{
    public class GetVoyagesQueryHandler : IQueryHandler<GetVoyagesQuery, IReadOnlyCollection<Domain.Model.VoyageModel.Voyage>>
    {
        private readonly IMsSqlConnection _msSqlConnection;
        private IVoyageQueries _voyageQueries;
        private ICarrierMovementQueries _carrierMovementQueries;


        public GetVoyagesQueryHandler(
            IMsSqlConnection msSqlConnection,
            IVoyageQueries voyageQueries,
            ICarrierMovementQueries carrierMovementQueries)
        {
            _msSqlConnection = msSqlConnection;
            _voyageQueries = voyageQueries;
            _carrierMovementQueries = carrierMovementQueries;
        }


        public async Task<IReadOnlyCollection<Domain.Model.VoyageModel.Voyage>> ExecuteQueryAsync(GetVoyagesQuery query, CancellationToken cancellationToken)
        {
            var voyageIdsBuilder = query.VoyageIds.Select(x => x.Value).ToList().CreateInQueryFromListId();
            Task<IReadOnlyCollection<VoyageReadModel>> getVoyagesByVoyageIds = _voyageQueries.GetVoyagesByIds(_msSqlConnection, voyageIdsBuilder, cancellationToken);
            Task<IReadOnlyCollection<CarrierMovementReadModel>> getCarrierMovementByVoyageIds = _carrierMovementQueries.GetCarrierMovementByAggregateId(_msSqlConnection, voyageIdsBuilder, cancellationToken);

            await Task.WhenAll(getVoyagesByVoyageIds, getCarrierMovementByVoyageIds);

            return getVoyagesByVoyageIds.Result.Select(x =>

                    x.ToVoyage(new VoyageId(x.AggregateId),
                                new Schedule(
                                    getCarrierMovementByVoyageIds.Result.Where(z => z.VoyageId == x.AggregateId)
                                    .Select(a => a.ToCarrierMovement()).ToList())
                                 )).ToList();
        }
    }
}
