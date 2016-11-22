using EventFlow.Specifications;
using Example.Shipping.Domain.Model.CargoModel.Entities;
using Example.Shipping.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.CargoModel.Specifications
{
    public class TransportLegsAreConnectedSpecification : Specification<IReadOnlyCollection<TransportLeg>>
    {
        protected override IEnumerable<string> IsNotSatisfiedBecause(IReadOnlyCollection<TransportLeg> obj)
        {
            return obj
                .Zip(obj.Skip(1), AreConnectedEvaluator)
                .SelectMany(s => s.ToList());
        }

        private static IEnumerable<string> AreConnectedEvaluator(TransportLeg previous, TransportLeg next)
        {
            if (previous.UnloadLocation != next.LoadLocation)
            {
                yield return Error(previous, next, $"Unload '{previous.UnloadLocation}' != load {next.LoadLocation}");
            }

            if (previous.UnloadTime.IsAfter(next.LoadTime))
            {
                yield return Error(previous, next, $"Unload '{previous.UnloadTime}' is after load {next.LoadTime}");
            }
        }

        private static string Error(TransportLeg previous, TransportLeg next, string validationError)
        {
            return $"{previous.Id} -> {next.Id}: {validationError}";
        }
    }
}
