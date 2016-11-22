namespace Example.Db
{
    using Model;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ExampleContext : DbContext
    {
        public ExampleContext()
            : base("name=ExampleContext")
        {
        }

        public virtual DbSet<Cargo> Cargo { get; set;}
        public virtual DbSet<CarrierMovement> CarrierMovement { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<TransportLeg> TransportLeg { get; set; }
        public virtual DbSet<Voyage> Voyage { get; set; }

    }


}