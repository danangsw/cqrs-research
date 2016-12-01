using EventFlow.Core;
using EventFlow.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Shipping.Queries.Mssql.Cargos.Queries
{
    public interface ITransportLegQueries
    {
        Task<IReadOnlyCollection<TransportLegReadModel>> GetTransportLegsByVoyageId(IMsSqlConnection _msSqlConnection, string voyageId, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<TransportLegReadModel>> GetTransportLegsByCargoIds(IMsSqlConnection _msSqlConnection, string cargoIds, CancellationToken cancellationToken);

    }

    public class TransportLegQueries : ITransportLegQueries
    {
        public async Task<IReadOnlyCollection<TransportLegReadModel>> GetTransportLegsByCargoIds(IMsSqlConnection _msSqlConnection, string cargoIds, CancellationToken cancellationToken)
        {
            var readTransportLegModels = await _msSqlConnection.QueryAsync<TransportLegReadModel>(
               Label.Named("mssql-fetch-transportlegs-read-model"),
               cancellationToken,
               "SELECT * FROM [TransportLeg] WHERE CargoId in @CargoIds",
                new { CargoIds = cargoIds })
               .ConfigureAwait(false);

            return readTransportLegModels;
        }

        public async Task<IReadOnlyCollection<TransportLegReadModel>> GetTransportLegsByVoyageId(IMsSqlConnection _msSqlConnection, string voyageId, CancellationToken cancellationToken)
        {
            var readTransportLegModels = await _msSqlConnection.QueryAsync<TransportLegReadModel>(
                Label.Named("mssql-fetch-transportlegs-read-model"),
                cancellationToken,
                "SELECT * FROM [TransportLeg] WHERE VoyageId = @VoyageId",
                 new { VoyageId = voyageId })
                .ConfigureAwait(false);

            return readTransportLegModels;
        }
    }
}
