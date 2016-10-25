using EventFlow.MsSql.ReadStores;
using EventFlow.Queries;
using Jmerp.Example.Shipping.Domain.Model.CargoModel;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jmerp.Example.Shipping.Queries.Mssql.Cargos.QueryHandlers
{
    public class GetCargosQueryHandler : IQueryHandler<GetCargosQuery, IReadOnlyCollection<Cargo>>
    {
        private readonly IMssqlReadModelStore<CargoReadModel> _readStore;

        public GetCargosQueryHandler(
            IMssqlReadModelStore<CargoReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<IReadOnlyCollection<Cargo>> ExecuteQueryAsync(GetCargosQuery query, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
