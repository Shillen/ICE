namespace ICE_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPredefinedMessageClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PredefinedMessage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmergencyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Emergency", t => t.EmergencyID, cascadeDelete: true)
                .Index(t => t.EmergencyID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PredefinedMessage", "EmergencyID", "dbo.Emergency");
            DropIndex("dbo.PredefinedMessage", new[] { "EmergencyID" });
            DropTable("dbo.PredefinedMessage");
        }
    }
}
