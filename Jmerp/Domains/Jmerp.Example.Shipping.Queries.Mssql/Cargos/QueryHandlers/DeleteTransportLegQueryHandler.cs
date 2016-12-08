using EventFlow.MsSql;
using EventFlow.Queries;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Queries;
using Jmerp.Example.Shipping.Queries.Mssql.Cargos.QueriesSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Queries.Mssql.Cargos.QueryHandlers
{
    public class DeleteTransportLegQueryHandler : IQueryHandler<DeleteTransportLegQuery, int>
    {

        private readonly IMsSqlConnection _msSqlConnection;
        private ITransportLegQueries _transportLegQueries;

        public DeleteTransportLegQueryHandler(
            IMsSqlConnection msSqlConnection,
             ITransportLegQueries transportLegQueries)
        {
            _msSqlConnection = msSqlConnection;
            _transportLegQueries = transportLegQueries;

        }

        public async Task<int> ExecuteQueryAsync(DeleteTransportLegQuery query, CancellationToken cancellationToken)
        {
            int intTransportLegQueries = await _transportLegQueries.DeleteTransportLegsByTransportLegId(_msSqlConnection, query.TransportLegId.Value, cancellationToken);
            return intTransportLegQueries;
        }
    }
}
