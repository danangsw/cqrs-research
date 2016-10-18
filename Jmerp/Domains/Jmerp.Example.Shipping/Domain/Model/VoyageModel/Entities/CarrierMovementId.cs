using EventFlow.Core;

namespace Jmerp.Example.Shipping.Domain.Model.VoyageModel.Entities
{
    public class CarrierMovementId : Identity<CarrierMovementId>
    {
        public CarrierMovementId(string value) : base(value)
        {
        }
    }
}
