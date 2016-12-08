using EventFlow.Core;
using EventFlow.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Queries.Mssql.Voyages.QueriesSql
{
    public interface ICarrierMovementQueries
    {
        Task<IReadOnlyCollection<CarrierMovementReadModel>> GetCarrierMovementByAggregateId(IMsSqlConnection _msSqlConnection, string[] inQueryVoyageIds, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<CarrierMovementReadModel>> GetAllCarrierMovement(IMsSqlConnection _msSqlConnection, CancellationToken cancellationToken);
    }


    public class CarrierMovementQueries : ICarrierMovementQueries
    {
        public async Task<IReadOnlyCollection<CarrierMovementReadModel>> GetCarrierMovementByAggregateId(IMsSqlConnection _msSqlConnection, string[] inQueryVoyageIds, CancellationToken cancellationToken)
        {
            var readCarrierMovementModels = await _msSqlConnection.QueryAsync<CarrierMovementReadModel>(
                Label.Named("mssql-fetch-carriermovement-read-model"),
                cancellationToken,
                "SELECT * FROM [CarrierMovement] WHERE VoyageId in @VoyageIds",
                 new { VoyageIds = inQueryVoyageIds })
                .ConfigureAwait(false);

            return readCarrierMovementModels;
        }

        public async Task<IReadOnlyCollection<CarrierMovementReadModel>> GetAllCarrierMovement(IMsSqlConnection _msSqlConnection, CancellationToken cancellationToken)
        {
            var readCarrierMovementModels = await _msSqlConnection.QueryAsync<CarrierMovementReadModel>(
                Label.Named("mssql-fetch-carriermovement-read-model"),
                cancellationToken,
                "SELECT * FROM [CarrierMovement]")
                .ConfigureAwait(false);

            return readCarrierMovementModels;
        }
    }
}
