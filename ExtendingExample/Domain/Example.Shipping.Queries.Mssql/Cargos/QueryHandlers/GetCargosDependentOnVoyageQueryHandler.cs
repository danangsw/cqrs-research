using EventFlow.Queries;
using Example.Shipping.Domain.Model.CargoModel;
using Example.Shipping.Domain.Model.CargoModel.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EventFlow.MsSql;
using Example.Shipping.Queries.Mssql.Cargos.Queries;
using Example.General.Extension;
using Example.Shipping.Domain.Model.CargoModel.ValueObjects;

namespace Example.Shipping.Queries.Mssql.Cargos.QueryHandlers
{
    public class GetCargosDependentOnVoyageQueryHandler : IQueryHandler<GetCargosDependentOnVoyageQuery, IReadOnlyCollection<Cargo>>
    {
        private readonly IMsSqlConnection _msSqlConnection;
        private ICargoQueries _cargoQueries;
        private ITransportLegQueries _transportLegQueries;

        public GetCargosDependentOnVoyageQueryHandler(
            IMsSqlConnection msSqlConnection,
            ICargoQueries cargoQueries,
            ITransportLegQueries transportLegQueries)
        {
            _msSqlConnection = msSqlConnection;
            _cargoQueries = cargoQueries;
            _transportLegQueries = transportLegQueries;
        }

        public async Task<IReadOnlyCollection<Cargo>> ExecuteQueryAsync(GetCargosDependentOnVoyageQuery query, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<TransportLegReadModel> getTransportLegsByVoyageId = await _transportLegQueries.GetTransportLegsByVoyageId(_msSqlConnection, query.VoyageId.Value , cancellationToken);
            string getCargoId = getTransportLegsByVoyageId.First().CargoId;

            Task<IReadOnlyCollection<CargoReadModel>> getCargo = _cargoQueries.GetCargoByCargoId(_msSqlConnection, getCargoId , cancellationToken);
            Task<IReadOnlyCollection<TransportLegReadModel>> getTransportLeg = _transportLegQueries.GetTransportLegsByCargoId(_msSqlConnection, getCargoId , cancellationToken);

            await Task.WhenAll(getCargo, getTransportLeg);

            return getCargo.Result.Select(x =>
                x.ToCargo(new CargoId(x.AggregateId),
                x.ToRoute(),
                new Itinerary(getTransportLeg.Result.Where(y=> y.CargoId == x.AggregateId).OrderBy(y=> y.UnloadTime)
                               .Select(z=> z.ToTransportLeg()).ToList())
              )).ToList();

        }
    }
}
