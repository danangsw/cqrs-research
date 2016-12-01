namespace Example.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cargo",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        OriginLocationId = c.String(),
                        DestinationLocationId = c.String(),
                        DepartureTime = c.DateTimeOffset(nullable: false, precision: 7),
                        ArrivalDeadline = c.DateTimeOffset(nullable: false, precision: 7),
                        AggregateId = c.String(maxLength: 64),
                        LastAggregateSequenceNumber = c.Int(nullable: false),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdatedTime = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.AggregateId, unique: true);
            
            CreateTable(
                "dbo.CarrierMovement",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        VoyageId = c.String(maxLength: 64),
                        CarrierMovementId = c.String(maxLength: 64),
                        DepartureLocationId = c.String(),
                        ArrivalLocationId = c.String(),
                        DepartureTime = c.DateTimeOffset(nullable: false, precision: 7),
                        ArrivalTime = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.VoyageId)
                .Index(t => t.CarrierMovementId, unique: true);
            
            CreateTable(
                "dbo.Location",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        AggregateId = c.String(maxLength: 64),
                        LastAggregateSequenceNumber = c.Int(nullable: false),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdatedTime = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.AggregateId, unique: true);
            
            CreateTable(
                "dbo.TransportLeg",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CargoId = c.String(maxLength: 64),
                        TransportLegId = c.String(maxLength: 64),
                        LoadLocation = c.String(),
                        UnloadLocation = c.String(),
                        LoadTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UnloadTime = c.DateTimeOffset(nullable: false, precision: 7),
                        VoyageId = c.String(maxLength: 64),
                        CarrierMovementId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.CargoId)
                .Index(t => t.TransportLegId, unique: true)
                .Index(t => t.VoyageId);
            
            CreateTable(
                "dbo.Voyage",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        AggregateId = c.String(maxLength: 64),
                        LastAggregateSequenceNumber = c.Int(nullable: false),
                        CreateTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UpdatedTime = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.AggregateId, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Voyage", new[] { "AggregateId" });
            DropIndex("dbo.TransportLeg", new[] { "VoyageId" });
            DropIndex("dbo.TransportLeg", new[] { "TransportLegId" });
            DropIndex("dbo.TransportLeg", new[] { "CargoId" });
            DropIndex("dbo.Location", new[] { "AggregateId" });
            DropIndex("dbo.CarrierMovement", new[] { "CarrierMovementId" });
            DropIndex("dbo.CarrierMovement", new[] { "VoyageId" });
            DropIndex("dbo.Cargo", new[] { "AggregateId" });
            DropTable("dbo.Voyage");
            DropTable("dbo.TransportLeg");
            DropTable("dbo.Location");
            DropTable("dbo.CarrierMovement");
            DropTable("dbo.Cargo");
        }
    }
}
