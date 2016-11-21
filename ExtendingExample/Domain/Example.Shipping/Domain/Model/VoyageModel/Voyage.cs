using EventFlow.Entities;
using Example.Shipping.Domain.Model.VoyageModel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Shipping.Domain.Model.VoyageModel
{
    public class Voyage : Entity<VoyageId>
    {
        public Voyage(
            VoyageId id,
            Schedule schedule)
            : base(id)
        {
            if (schedule == null) throw new ArgumentNullException(nameof(schedule));

            Schedule = schedule;
        }

        public Schedule Schedule { get; }
    }
}
