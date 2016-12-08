using EventFlow.Core;
using EventFlow.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Queries.Mssql.Cargos.QueriesSql
{
    public interface ICargoQueries
    {
        Task<IReadOnlyCollection<CargoReadModel>> GetCargosByCargoIds(IMsSqlConnection _msSqlConnection, string[] cargoIds, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<CargoReadModel>> GetCargoByCargoId(IMsSqlConnection _msSqlConnection, string cargoId, CancellationToken cancellationToken);

    }

    public class CargoQueries : ICargoQueries
    {
        public async Task<IReadOnlyCollection<CargoReadModel>> GetCargoByCargoId(IMsSqlConnection _msSqlConnection, string cargoId, CancellationToken cancellationToken)
        {
            var readCargoModels = await _msSqlConnection.QueryAsync<CargoReadModel>(
                Label.Named("mssql-fetch-cargo-read-model"),
                cancellationToken,
                "SELECT * FROM [Cargo] WHERE AggregateId = @AggregateId",
                 new { AggregateId = cargoId })
                .ConfigureAwait(false);

            return readCargoModels;
        }

        public async Task<IReadOnlyCollection<CargoReadModel>> GetCargosByCargoIds(IMsSqlConnection _msSqlConnection, string[] cargoIds, CancellationToken cancellationToken)
        {
            var readCargoModels = await _msSqlConnection.QueryAsync<CargoReadModel>(
                Label.Named("mssql-fetch-cargos-read-model"),
                cancellationToken,
                "SELECT * FROM [Cargo] WHERE AggregateId in @AggregateIds",
                 new { AggregateIds = cargoIds })
                .ConfigureAwait(false);

            return readCargoModels;
        }
    }
}
