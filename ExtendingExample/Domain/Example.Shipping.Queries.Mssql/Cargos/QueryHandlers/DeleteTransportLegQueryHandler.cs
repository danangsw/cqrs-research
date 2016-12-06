using EventFlow.Queries;
using Example.Shipping.Domain.Model.CargoModel.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EventFlow.MsSql;
using Example.Shipping.Queries.Mssql.Cargos.Queries;

namespace Example.Shipping.Queries.Mssql.Cargos.QueryHandlers
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
            int intTransportLegQueries = await _transportLegQueries.DeleteTransportLegsByTransportLegId(_msSqlConnection, query.TransportLegId.Value , cancellationToken);
            return intTransportLegQueries;
        }
    }
}
