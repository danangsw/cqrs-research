namespace Jmerp.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReadModel-Cargo",
                c => new
                    {
                        Index = c.Long(nullable: false, identity: true),
                        Id = c.String(),
                        Voyage_Index = c.Long(nullable: false),
                        OriginLocationId = c.String(),
                        DestinationLocationId = c.String(),
                        DepartureTime = c.DateTimeOffset(nullable: false, precision: 7),
                        ArrivalDeadline = c.DateTimeOffset(nullable: false, precision: 7),
                        MsSqlReadModelVersionColumn = c.Int(),
                    })
                .PrimaryKey(t => t.Index)
                .ForeignKey("dbo.ReadModel-Voyage", t => t.Voyage_Index, cascadeDelete: true)
                .Index(t => t.Voyage_Index);
            
            CreateTable(
                "dbo.ReadModel-Voyage",
                c => new
                    {
                        Index = c.Long(nullable: false, identity: true),
                        Id = c.String(),
                        MsSqlReadModelVersionColumn = c.Int(),
                    })
                .PrimaryKey(t => t.Index);
            
            CreateTable(
                "dbo.CarrierMovements",
                c => new
                    {
                        Index = c.Long(nullable: false, identity: true),
                        Id = c.String(),
                        DepartureLocationId = c.String(),
                        ArrivalLocationId = c.String(),
                        DepartureTime = c.DateTimeOffset(nullable: false, precision: 7),
                        ArrivalTime = c.DateTimeOffset(nullable: false, precision: 7),
                        Voyage_Index = c.Long(),
                    })
                .PrimaryKey(t => t.Index)
                .ForeignKey("dbo.ReadModel-Voyage", t => t.Voyage_Index)
                .Index(t => t.Voyage_Index);
            
            CreateTable(
                "dbo.TransportLegs",
                c => new
                    {
                        Index = c.Long(nullable: false, identity: true),
                        Id = c.String(),
                        LoadLocation = c.String(),
                        UnloadLocation = c.String(),
                        LoadTime = c.DateTimeOffset(nullable: false, precision: 7),
                        UnloadTime = c.DateTimeOffset(nullable: false, precision: 7),
                        Voyage_Index = c.Long(nullable: false),
                        CarrierMovement_Index = c.Long(nullable: false),
                        Cargo_Index = c.Long(),
                    })
                .PrimaryKey(t => t.Index)
                .ForeignKey("dbo.ReadModel-Cargo", t => t.Cargo_Index)
                .ForeignKey("dbo.CarrierMovements", t => t.CarrierMovement_Index, cascadeDelete: true)
                .ForeignKey("dbo.ReadModel-Voyage", t => t.Voyage_Index, cascadeDelete: true)
                .Index(t => t.Voyage_Index)
                .Index(t => t.CarrierMovement_Index)
                .Index(t => t.Cargo_Index);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransportLegs", "Voyage_Index", "dbo.ReadModel-Voyage");
            DropForeignKey("dbo.TransportLegs", "CarrierMovement_Index", "dbo.CarrierMovements");
            DropForeignKey("dbo.TransportLegs", "Cargo_Index", "dbo.ReadModel-Cargo");
            DropForeignKey("dbo.CarrierMovements", "Voyage_Index", "dbo.ReadModel-Voyage");
            DropForeignKey("dbo.ReadModel-Cargo", "Voyage_Index", "dbo.ReadModel-Voyage");
            DropIndex("dbo.TransportLegs", new[] { "Cargo_Index" });
            DropIndex("dbo.TransportLegs", new[] { "CarrierMovement_Index" });
            DropIndex("dbo.TransportLegs", new[] { "Voyage_Index" });
            DropIndex("dbo.CarrierMovements", new[] { "Voyage_Index" });
            DropIndex("dbo.ReadModel-Cargo", new[] { "Voyage_Index" });
            DropTable("dbo.TransportLegs");
            DropTable("dbo.CarrierMovements");
            DropTable("dbo.ReadModel-Voyage");
            DropTable("dbo.ReadModel-Cargo");
        }
    }
}
