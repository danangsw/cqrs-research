using EventFlow.Queries;
using Example.Shipping.Domain.Model.VoyageModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.CargoModel.Queries
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
