using EventFlow.Entities;
using Jmerp.Example.Shipping.Domain.Model.VoyageModel.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jmerp.Example.Shipping.Domain.Model.VoyageModel
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
