namespace ICE_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBroadcastClassToContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Broadcast",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Time = c.DateTime(nullable: false),
                        EmergencyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Emergency", t => t.EmergencyId, cascadeDelete: true)
                .Index(t => t.EmergencyId);
            
            AddColumn("dbo.Building", "Broadcast_ID", c => c.Int());
            CreateIndex("dbo.Building", "Broadcast_ID");
            AddForeignKey("dbo.Building", "Broadcast_ID", "dbo.Broadcast", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Broadcast", "EmergencyId", "dbo.Emergency");
            DropForeignKey("dbo.Building", "Broadcast_ID", "dbo.Broadcast");
            DropIndex("dbo.Building", new[] { "Broadcast_ID" });
            DropIndex("dbo.Broadcast", new[] { "EmergencyId" });
            DropColumn("dbo.Building", "Broadcast_ID");
            DropTable("dbo.Broadcast");
        }
    }
}
