using EventFlow.Queries;
using Example.Shipping.Domain.Model.CargoModel.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Example.Shipping.Domain.Model.CargoModel;

namespace Example.Shipping.Queries.Mssql.Cargos.QueryHandlers
{
    public class GetCargosQueryHandler : IQueryHandler<GetCargosQuery, IReadOnlyCollection<Cargo>>
    {
        public Task<IReadOnlyCollection<Cargo>> ExecuteQueryAsync(GetCargosQuery query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
