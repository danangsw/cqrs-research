using EventFlow.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.VoyageModel.Queries
{
    public class GetAllVoyagesQuery : IQuery<IReadOnlyCollection<Voyage>>
    {

    }
}
