using EventFlow.MsSql.ReadStores;
using EventFlow.Queries;
using EventFlow.ReadStores.InMemory;
using Jmerp.Example.Shipping.Domain.Model.CargoModel;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;



namespace Jmerp.Example.Shipping.Queries.Mssql.Cargos.QueryHandlers
{
    public class GetCargosDependentOnVoyageQueryHandler : IQueryHandler<GetCargosDependentOnVoyageQuery, IReadOnlyCollection<Cargo>>
    {
        private readonly IMssqlReadModelStore<CargoReadModel> _readStore;

        public GetCargosDependentOnVoyageQueryHandler(
            IMssqlReadModelStore<CargoReadModel> readStore)
        {
            _readStore = readStore;
        }

        public async Task<IReadOnlyCollection<Cargo>> ExecuteQueryAsync(GetCargosDependentOnVoyageQuery query, CancellationToken cancellationToken)
        {

            return  null;
        }
    }
}
