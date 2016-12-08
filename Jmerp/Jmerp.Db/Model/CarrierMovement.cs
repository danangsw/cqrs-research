using Jmerp.Commons.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Db.Model
{
    [Table("CarrierMovement")]
    public class CarrierMovement : BaseEntity
    {
        [StringLength(64)]
        [Index(IsUnique = false, Order = 1)]
        public string VoyageId { get; set; }
        [StringLength(64)]
        [Index(IsUnique = true, Order = 2)]
        public string CarrierMovementId { get; set; }
        public string DepartureLocationId { get; set; }
        public string ArrivalLocationId { get; set; }
        public DateTimeOffset DepartureTime { get; set; }
        public DateTimeOffset ArrivalTime { get; set; }
    }
}
