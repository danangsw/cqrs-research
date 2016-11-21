using EventFlow.Queries;
using Example.Shipping.Domain.Model.VoyageModel.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Example.Shipping.Domain.Model.VoyageModel;
using System.Threading;
using EventFlow.MsSql;

namespace Example.Shipping.Queries.Mssql.Voyage.QueryHandlers
{
    public class GetAllVoyagesQueryHandler : IQueryHandler<GetAllVoyagesQuery, IReadOnlyCollection<Domain.Model.VoyageModel.Voyage>>
    {

        private readonly IMsSqlConnection _msSqlConnection;

        public GetAllVoyagesQueryHandler(
            IMsSqlConnection msSqlConnection)
        {
            _msSqlConnection = msSqlConnection;
        }

        public Task<IReadOnlyCollection<Domain.Model.VoyageModel.Voyage>> ExecuteQueryAsync(GetAllVoyagesQuery query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
