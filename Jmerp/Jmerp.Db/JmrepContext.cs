using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Db
{
    using Model;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class JmrepContext : DbContext
    {
        public JmrepContext()
            : base("name=JmrepContext")
        {
        }

        public virtual DbSet<Cargo> Cargo { get; set; }
        public virtual DbSet<CarrierMovement> CarrierMovement { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<TransportLeg> TransportLeg { get; set; }
        public virtual DbSet<Voyage> Voyage { get; set; }

    }
}
