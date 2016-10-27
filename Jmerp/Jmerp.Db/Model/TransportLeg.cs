using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Db.Model
{
    public class TransportLeg : BaseEntity
    {
        public string Id { get; set; }

        public string LoadLocation { get; set; }
        public string UnloadLocation { get; set; }
        public DateTimeOffset LoadTime { get; set; }
        public DateTimeOffset UnloadTime { get; set; }
        public long Voyage_Index { get; set; }
        public long CarrierMovement_Index { get; set; }

        [ForeignKey("Voyage_Index")]
        public Voyage Voyage { get; set; }

        [ForeignKey("CarrierMovement_Index")]
        public CarrierMovement CarrierMovement { get; set; }

        public Cargo Cargo { get; set; }
    }
}
