using EventFlow.MsSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Shipping.Queries.Mssql.Voyage.Queries
{
    public interface IVoyageQueries
    {
        Task<IReadOnlyCollection<VoyageReadModel>> GetAllVoyages(IMsSqlConnection _msSqlConnection, CancellationToken cancellationToken);
    }
}
