using Jmerp.Commons.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Db.Model
{
    [Table("Cargo")]
    public class Cargo : BaseAggregate
    {
        public string OriginLocationId { get; set; }
        public string DestinationLocationId { get; set; }
        public DateTimeOffset DepartureTime { get; set; }
        public DateTimeOffset ArrivalDeadline { get; set; }
    }
}
