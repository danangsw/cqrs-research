using EventFlow.Core;
using EventFlow.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Shipping.Queries.Mssql.Voyage.Queries
{
    public class VoyageQueries : IVoyageQueries
    {
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
