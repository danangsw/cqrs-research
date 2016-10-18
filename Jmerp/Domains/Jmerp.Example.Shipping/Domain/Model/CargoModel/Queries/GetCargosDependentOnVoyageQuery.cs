using EventFlow.Queries;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel;
using System.Collections.Generic;

namespace Jmerp.Example.Shipping.Domain.Model.CargoModel.Queries
{
    public class GetCargosDependentOnVoyageQuery : IQuery<IReadOnlyCollection<Cargo>>
    {
        public GetCargosDependentOnVoyageQuery(VoyageId voyageId)
        {
            VoyageId = voyageId;
        }

        public VoyageId VoyageId { get; }
    }
}
