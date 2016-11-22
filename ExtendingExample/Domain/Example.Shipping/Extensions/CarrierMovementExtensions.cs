using Example.Shipping.Domain.Model.VoyageModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Extensions
{
    public static class CarrierMovementExtensions
    {
        public static TimeSpan TravelTime(this CarrierMovement carrierMovement)
        {
            return carrierMovement.ArrivalTime - carrierMovement.DepartureTime;
        }
    }
}
