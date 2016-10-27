namespace Jmerp.Db
{
    using Model;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;

    public class JmrepContext : DbContext
    {

        public JmrepContext()
            : base("name=JmrepContext")
        {
        }

        public virtual DbSet<CarrierMovement> CarrierMovement { get; set; }
        public virtual DbSet<TransportLeg> TransportLeg { get; set; }
        public virtual DbSet<Cargo> Cargo { get; set; }
        public virtual DbSet<Voyage> Voyage { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Cargo>().ToTable("ReadModel-Cargo");
            modelBuilder.Entity<Voyage>().ToTable("ReadModel-Voyage");
        }

        public virtual async Task CommitAsync()
        {
            await base.SaveChangesAsync();
        }

        
        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            return base.ValidateEntity(entityEntry, items);
        }

    }


}