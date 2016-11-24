using EventFlow.Core;
using EventFlow.MsSql;
using Example.Shipping.Domain.Model.VoyageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Shipping.Queries.Mssql.Voyage.Queries
{
    public class CarrierMovementQueries : ICarrierMovementQueries
    {
        public async Task<IReadOnlyCollection<CarrierMovementReadModel>> GetCarrierMovementByAggregateId(IMsSqlConnection _msSqlConnection , VoyageId voyageId, CancellationToken cancellationToken)
        {
            var readCarrierMovementModels = await _msSqlConnection.QueryAsync<CarrierMovementReadModel>(
                Label.Named("mssql-fetch-carriermovement-read-model"),
                cancellationToken,
                "SELECT * FROM [CarrierMovement] WHERE AggregateId = @AggregateId",
                 new { AggregateId = voyageId.Value })
                .ConfigureAwait(false);

            return readCarrierMovementModels;
        }

        public async Task<IReadOnlyCollection<CarrierMovementReadModel>> GetAllCarrierMovement (IMsSqlConnection _msSqlConnection, CancellationToken cancellationToken)
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
