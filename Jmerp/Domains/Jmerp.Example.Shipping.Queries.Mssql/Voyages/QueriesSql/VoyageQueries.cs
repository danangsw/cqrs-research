﻿using EventFlow.Core;
using EventFlow.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Queries.Mssql.Voyages.QueriesSql
{
    public interface IVoyageQueries
    {
        Task<IReadOnlyCollection<VoyageReadModel>> GetVoyagesByIds(IMsSqlConnection msSqlConnection, string[] inQueryVoyageIds, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<VoyageReadModel>> GetAllVoyages(IMsSqlConnection _msSqlConnection, CancellationToken cancellationToken);
    }


    public class VoyageQueries : IVoyageQueries
    {

        public async Task<IReadOnlyCollection<VoyageReadModel>> GetVoyagesByIds(IMsSqlConnection msSqlConnection, string[] inQueryVoyageIds, CancellationToken cancellationToken)
        {
            var readVoyageModels = await msSqlConnection.QueryAsync<VoyageReadModel>(
                Label.Named("mssql-fetch-voyages-read-model"),
                cancellationToken,
                "SELECT * FROM [Voyage] WHERE AggregateId in @AggregateIds",
                 new { AggregateIds = inQueryVoyageIds })
                .ConfigureAwait(false);

            return readVoyageModels;
        }

        public async Task<IReadOnlyCollection<VoyageReadModel>> GetAllVoyages(IMsSqlConnection msSqlConnection, CancellationToken cancellationToken)
        {
            var readVoyageModels = await msSqlConnection.QueryAsync<VoyageReadModel>(
                Label.Named("mssql-fetch-voyages-read-model"),
                cancellationToken,
                "SELECT * FROM [Voyage]")
                .ConfigureAwait(false);

            return readVoyageModels;
        }
    }
}
