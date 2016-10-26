using System;
using System.Collections.Generic;
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
        public string VoyageId { get; set; }
        public string CarrierMovementId { get; set; }
        public Cargo Cargo { get; set; }
    }
}
