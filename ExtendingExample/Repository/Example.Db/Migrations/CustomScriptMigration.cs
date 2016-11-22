using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Db.Migrations
{
    public class CustomScriptMigration : DbMigration
    {
        public override void Down()
        {
            DropIndex("dbo.Voyage", new[] { "AggregateId" });
            DropIndex("dbo.TransportLeg", new[] { "TransportLegId" });
            DropIndex("dbo.TransportLeg", new[] { "CargoId" });
            DropIndex("dbo.Location", new[] { "AggregateId" });
            DropIndex("dbo.CarrierMovement", new[] { "CarrierMovementId" });
            DropIndex("dbo.CarrierMovement", new[] { "CargoId" });
            DropIndex("dbo.Cargo", new[] { "AggregateId" });
            DropTable("dbo.Voyage");
            DropTable("dbo.TransportLeg");
            DropTable("dbo.Location");
            DropTable("dbo.CarrierMovement");
            DropTable("dbo.Cargo");
        }

        public override void Up()
        {
            
        }
    }
}
