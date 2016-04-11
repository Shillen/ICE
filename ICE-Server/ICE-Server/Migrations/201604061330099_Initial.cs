namespace ICE_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
            
            CreateTable(
                "dbo.Building",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Broadcast_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Broadcast", t => t.Broadcast_ID)
                .Index(t => t.Broadcast_ID);
            
            CreateTable(
                "dbo.Emergency",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Device",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DeviceID = c.String(maxLength: 200),
                        DeviceOS = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmergencyTranslated",
                c => new
                    {
                        EmergencyID = c.Int(nullable: false),
                        LanguageID = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => new { t.EmergencyID, t.LanguageID })
                .ForeignKey("dbo.Emergency", t => t.EmergencyID, cascadeDelete: true)
                .ForeignKey("dbo.Language", t => t.LanguageID, cascadeDelete: true)
                .Index(t => t.EmergencyID)
                .Index(t => t.LanguageID);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
            
            CreateTable(
                "dbo.PredefinedMessageTranslated",
                c => new
                    {
                        PredefinedMessageID = c.Int(nullable: false),
                        LanguageID = c.Int(nullable: false),
                        Message = c.String(),
                    })
                .PrimaryKey(t => new { t.PredefinedMessageID, t.LanguageID })
                .ForeignKey("dbo.Language", t => t.LanguageID, cascadeDelete: true)
                .ForeignKey("dbo.PredefinedMessage", t => t.PredefinedMessageID, cascadeDelete: true)
                .Index(t => t.PredefinedMessageID)
                .Index(t => t.LanguageID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PredefinedMessageTranslated", "PredefinedMessageID", "dbo.PredefinedMessage");
            DropForeignKey("dbo.PredefinedMessageTranslated", "LanguageID", "dbo.Language");
            DropForeignKey("dbo.PredefinedMessage", "EmergencyID", "dbo.Emergency");
            DropForeignKey("dbo.EmergencyTranslated", "LanguageID", "dbo.Language");
            DropForeignKey("dbo.EmergencyTranslated", "EmergencyID", "dbo.Emergency");
            DropForeignKey("dbo.Broadcast", "EmergencyId", "dbo.Emergency");
            DropForeignKey("dbo.Building", "Broadcast_ID", "dbo.Broadcast");
            DropIndex("dbo.PredefinedMessageTranslated", new[] { "LanguageID" });
            DropIndex("dbo.PredefinedMessageTranslated", new[] { "PredefinedMessageID" });
            DropIndex("dbo.PredefinedMessage", new[] { "EmergencyID" });
            DropIndex("dbo.EmergencyTranslated", new[] { "LanguageID" });
            DropIndex("dbo.EmergencyTranslated", new[] { "EmergencyID" });
            DropIndex("dbo.Building", new[] { "Broadcast_ID" });
            DropIndex("dbo.Broadcast", new[] { "EmergencyId" });
            DropTable("dbo.PredefinedMessageTranslated");
            DropTable("dbo.PredefinedMessage");
            DropTable("dbo.Language");
            DropTable("dbo.EmergencyTranslated");
            DropTable("dbo.Device");
            DropTable("dbo.Emergency");
            DropTable("dbo.Building");
            DropTable("dbo.Broadcast");
        }
    }
}
