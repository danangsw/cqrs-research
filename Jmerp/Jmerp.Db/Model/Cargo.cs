using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Db.Model
{
    public class Cargo : BaseEntity
    {
        public string Id { get; set; }
        public string DependentVoyageIds { get; set; }

        public Itinerary Itinerary { get; set; }

        public Route Route { get; set; }

    }
}
