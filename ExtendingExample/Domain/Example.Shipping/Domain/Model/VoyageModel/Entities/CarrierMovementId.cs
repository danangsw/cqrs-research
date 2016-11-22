using EventFlow.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.VoyageModel.Entities
{
    public class CarrierMovementId : Identity<CarrierMovementId>
    {
        public CarrierMovementId(string value) : base(value)
        {
        }
    }
}
