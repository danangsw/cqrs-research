using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Db.Model
{
    [ComplexType]
    public class Route
    {
        [Column("OriginLocationId")]
        public string OriginLocationId { get; set; }
        [Column("DestinationLocationId")]
        public string DestinationLocationId { get; set; }
        [Column("DepartureTime")]
        public DateTimeOffset DepartureTime { get; set; }
        [Column("ArrivalDeadline")]
        public DateTimeOffset ArrivalDeadline { get; set; }
    }
}
