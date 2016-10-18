using Jmerp.Example.Shipping.Domain.Model.VoyageModel.Entities;
using System;

namespace Jmerp.Example.Shipping.Extensions
{
    public static class CarrierMovementExtensions
    {
        public static TimeSpan TravelTime(this CarrierMovement carrierMovement)
        {
            return carrierMovement.ArrivalTime - carrierMovement.DepartureTime;
        }
    }
}
