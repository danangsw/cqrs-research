using EventFlow.MsSql;
using EventFlow.MsSql.ReadStores;
using EventFlow.Queries;
using Jmerp.Commons.Extension;
using Jmerp.Example.Shipping.Domain.Model.CargoModel;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.Queries;
using Jmerp.Example.Shipping.Domain.Model.CargoModel.ValueObjects;
using Jmerp.Example.Shipping.Queries.Mssql.Cargos.QueriesSql;
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
        private readonly IMsSqlConnection _msSqlConnection;
        private ICargoQueries _cargoQueries;
        private ITransportLegQueries _transportLegQueries;

        public GetCargosQueryHandler(
            IMsSqlConnection msSqlConnection,
            ICargoQueries cargoQueries,
            ITransportLegQueries transportLegQueries)
        {
            _msSqlConnection = msSqlConnection;
            _cargoQueries = cargoQueries;
            _transportLegQueries = transportLegQueries;
        }

        public async Task<IReadOnlyCollection<Cargo>> ExecuteQueryAsync(GetCargosQuery query, CancellationToken cancellationToken)
        {
            var inCargoQuery = query.CargoIds.Select(x => x.Value).ToList().CreateInQueryFromListId();
            Task<IReadOnlyCollection<CargoReadModel>> getCargosByCargoIds = _cargoQueries.GetCargosByCargoIds(_msSqlConnection, inCargoQuery, cancellationToken);
            Task<IReadOnlyCollection<TransportLegReadModel>> getTransportLegsByCargoIds = _transportLegQueries.GetTransportLegsByCargoIds(_msSqlConnection, inCargoQuery, cancellationToken);

            await Task.WhenAll(getCargosByCargoIds, getTransportLegsByCargoIds);

            return getCargosByCargoIds.Result.Select(x =>
                x.ToCargo(new CargoId(x.AggregateId),
                x.ToRoute(),
                new Itinerary(getTransportLegsByCargoIds.Result.Where(y => y.CargoId == x.AggregateId).OrderBy(y => y.UnloadTime)
                               .Select(z => z.ToTransportLeg()).ToList())
              )).ToList();
        }
    }
}
