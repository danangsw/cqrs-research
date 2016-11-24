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
using EventFlow.Core;
using Example.Shipping.Queries.Mssql.Voyage.Queries;
using Example.Shipping.Domain.Model.VoyageModel.ValueObjects;

namespace Example.Shipping.Queries.Mssql.Voyage.QueryHandlers
{
    public class GetAllVoyagesQueryHandler : IQueryHandler<GetAllVoyagesQuery, IReadOnlyCollection<Domain.Model.VoyageModel.Voyage>>
    {

        private readonly IMsSqlConnection _msSqlConnection;
        private IVoyageQueries _voyageQueries;
        private ICarrierMovementQueries _carrierMovementQueries;

        public GetAllVoyagesQueryHandler(
            IMsSqlConnection msSqlConnection,
            IVoyageQueries voyageQueries,
            ICarrierMovementQueries carrierMovementQueries)
        {
            _msSqlConnection = msSqlConnection;
            _voyageQueries = voyageQueries;
            _carrierMovementQueries = carrierMovementQueries;
        }

        public async Task<IReadOnlyCollection<Domain.Model.VoyageModel.Voyage>> ExecuteQueryAsync(GetAllVoyagesQuery query, CancellationToken cancellationToken)
        {
            Task<IReadOnlyCollection<VoyageReadModel>> getAllVoyages = _voyageQueries.GetAllVoyages(_msSqlConnection, cancellationToken);
            Task<IReadOnlyCollection<CarrierMovementReadModel>> getCarrierMovement = _carrierMovementQueries.GetAllCarrierMovement(_msSqlConnection, cancellationToken);


            await Task.WhenAll(getAllVoyages, getCarrierMovement);

            return getAllVoyages.Result.Select(x =>

                    x.ToVoyage(new VoyageId(x.AggregateId),
                                new Schedule(
                                    getCarrierMovement.Result.Where(z => z.VoyageId == x.AggregateId)
                                    .Select(a => a.ToCarrierMovement()).ToList())
                                 )).ToList();
        }


    }
}
