using EventFlow.MsSql;
using EventFlow.MsSql.ReadStores;
using EventFlow.Queries;
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
