using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Commons.Database
{
    public abstract class BaseAggregate : BaseEntity
    {
        [StringLength(64)]
        [Index(IsUnique = true, Order = 1)]
        public string AggregateId { get; set; }

        public int LastAggregateSequenceNumber { get; set; }

        public DateTimeOffset CreateTime { get; set; }

        public DateTimeOffset UpdatedTime { get; set; }

    }
}
