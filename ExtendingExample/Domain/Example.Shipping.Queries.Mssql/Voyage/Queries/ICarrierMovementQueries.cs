using EventFlow.MsSql;
using Example.Shipping.Domain.Model.VoyageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Example.Shipping.Queries.Mssql.Voyage.Queries
{
    public interface ICarrierMovementQueries
    {
        Task<IReadOnlyCollection<CarrierMovementReadModel>> GetCarrierMovementByAggregateId(IMsSqlConnection _msSqlConnection, VoyageId VoyageId, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<CarrierMovementReadModel>> GetAllCarrierMovement(IMsSqlConnection _msSqlConnection, CancellationToken cancellationToken);
    }
}
