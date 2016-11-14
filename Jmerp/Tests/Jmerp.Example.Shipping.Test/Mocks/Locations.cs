using Jmerp.Example.Shipping.Domain.Model.LocationModel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jmerp.Example.Shipping.Tests.Mocks
{
    public static class Locations
    {
        public static readonly LocationId Hongkong = new LocationId("CNHKG");
        public static readonly LocationId Melbourne = new LocationId("AUMEL");
        public static readonly LocationId Stockholm = new LocationId("SESTO");
        public static readonly LocationId Helsinki = new LocationId("FIHEL");
        public static readonly LocationId Chicago = new LocationId("USCHI");
        public static readonly LocationId Tokyo = new LocationId("JNTKO");
        public static readonly LocationId Hamburg = new LocationId("DEHAM");
        public static readonly LocationId Shanghai = new LocationId("CNSHA");
        public static readonly LocationId Rotterdam = new LocationId("NLRTM");
        public static readonly LocationId Gothenburg = new LocationId("SEGOT");
        public static readonly LocationId Hangzou = new LocationId("CNHGH");
        public static readonly LocationId NewYork = new LocationId("USNYC");
        public static readonly LocationId Dallas = new LocationId("USDAL");

        public static IEnumerable<Location> GetLocations()
        {
            var fieldInfos = typeof(Locations).GetFields(BindingFlags.Public | BindingFlags.Static);
            return fieldInfos.Select(fi => new Location((LocationId)fi.GetValue(null), fi.Name));
        }
    }
}
