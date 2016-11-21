using Example.General.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Db.Model
{
    [Table("TransportLeg")]
    public class TransportLeg : BaseEntity
    {
        [StringLength(64)]
        [Index(IsUnique = false, Order = 1)]
        public string CargoId { get; set; }

        [StringLength(64)]
        [Index(IsUnique = true, Order = 2)]
        public string TransportLegId { get; set; }
        public string LoadLocation { get; set; }
        public string UnloadLocation { get; set; }
        public DateTimeOffset LoadTime { get; set; }
        public DateTimeOffset UnloadTime { get; set; }

        public string VoyageId { get; set; }
        public string CarrierMovementId { get; set; }

    }
}
