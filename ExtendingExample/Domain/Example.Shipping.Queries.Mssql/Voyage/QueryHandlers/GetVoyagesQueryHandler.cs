using EventFlow.Queries;
using Example.Shipping.Domain.Model.VoyageModel.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Shipping.Domain.Model.VoyageModel;
using System.Threading;
using EventFlow.MsSql;
using Example.Shipping.Queries.Mssql.Voyage.Queries;
using Example.Shipping.Domain.Model.VoyageModel.ValueObjects;
using Example.General.Extension;


namespace Example.Shipping.Queries.Mssql.Voyage.QueryHandlers
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
            var voyageIdsBuilder = query.VoyageIds.Select(x=>x.Value).ToList().CreateInQueryFromListId();
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
