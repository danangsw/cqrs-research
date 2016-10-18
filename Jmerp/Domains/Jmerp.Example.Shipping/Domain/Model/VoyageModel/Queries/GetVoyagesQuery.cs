using EventFlow.Queries;
using System.Collections.Generic;
using System.Linq;

namespace Jmerp.Example.Shipping.Domain.Model.VoyageModel.Queries
{
    public class GetVoyagesQuery : IQuery<IReadOnlyCollection<Voyage>>
    {
        public GetVoyagesQuery(
            IEnumerable<VoyageId> voyageIds)
        {
            VoyageIds = voyageIds.ToList();
        }

        public IReadOnlyCollection<VoyageId> VoyageIds { get; }
    }
}
