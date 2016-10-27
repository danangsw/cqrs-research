using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Db.Model
{
    public class Cargo : BaseEntity, IReadModelEntity
    {
        public string Id { get; set; }
        public long Voyage_Index { get; set; }

        public Itinerary Itinerary { get; set; }

        public Route Route { get; set; }

        [ForeignKey("Voyage_Index")]
        public Voyage Voyage { get; set; }

        public int? MsSqlReadModelVersionColumn { get; set; }
    }
}
