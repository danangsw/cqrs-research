using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Db.Model
{
    public class CarrierMovement : BaseEntity
    {
        public string Id { get; set; }
        public string DepartureLocationId { get; set; }
        public string ArrivalLocationId { get; set; }
        public DateTimeOffset DepartureTime { get; set; }
        public DateTimeOffset ArrivalTime { get; set; }
        public virtual Voyage Voyage { get; set; }
    }
}
