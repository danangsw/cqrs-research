
using EventFlow.Entities;

namespace Jmerp.Example.Shipping.Domain.Model.LocationModel
{
    public class Location : Entity<LocationId>
    {
        public Location(
            LocationId id,
            string name)
            : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
