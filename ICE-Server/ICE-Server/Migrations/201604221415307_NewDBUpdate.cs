namespace ICE_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDBUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Building", "Broadcast_ID", "dbo.Broadcast");
            DropIndex("dbo.Building", new[] { "Broadcast_ID" });
            CreateTable(
                "dbo.BroadcastBuilding",
                c => new
                    {
                        BroadcastID = c.Int(nullable: false),
                        BuildingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BroadcastID, t.BuildingID })
                .ForeignKey("dbo.Broadcast", t => t.BroadcastID, cascadeDelete: true)
                .ForeignKey("dbo.Building", t => t.BuildingID, cascadeDelete: true)
                .Index(t => t.BroadcastID)
                .Index(t => t.BuildingID);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Option = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Building", "Location", c => c.String());
            DropColumn("dbo.Building", "Broadcast_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Building", "Broadcast_ID", c => c.Int());
            DropForeignKey("dbo.BroadcastBuilding", "BuildingID", "dbo.Building");
            DropForeignKey("dbo.BroadcastBuilding", "BroadcastID", "dbo.Broadcast");
            DropIndex("dbo.BroadcastBuilding", new[] { "BuildingID" });
            DropIndex("dbo.BroadcastBuilding", new[] { "BroadcastID" });
            DropColumn("dbo.Building", "Location");
            DropTable("dbo.Settings");
            DropTable("dbo.BroadcastBuilding");
            CreateIndex("dbo.Building", "Broadcast_ID");
            AddForeignKey("dbo.Building", "Broadcast_ID", "dbo.Broadcast", "ID");
        }
    }
}
