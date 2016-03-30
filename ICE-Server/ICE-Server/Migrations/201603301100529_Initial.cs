namespace ICE_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Broadcasts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Buildings = c.String(),
                        Time = c.DateTime(nullable: false),
                        EmergencyID = c.Int(nullable: false),
                        EmergencyTypes_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EmergencyTypes", t => t.EmergencyTypes_ID)
                .Index(t => t.EmergencyTypes_ID);
            
            CreateTable(
                "dbo.EmergencyTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.EmergencyTranslated",
                c => new
                    {
                        EmergencyID = c.Int(nullable: false, identity: true),
                        LanguageID = c.Int(nullable: false),
                        Name = c.String(),
                        EmergencyTypes_ID = c.Int(),
                    })
                .PrimaryKey(t => t.EmergencyID)
                .ForeignKey("dbo.EmergencyTypes", t => t.EmergencyTypes_ID)
                .ForeignKey("dbo.Language", t => t.LanguageID, cascadeDelete: true)
                .Index(t => t.LanguageID)
                .Index(t => t.EmergencyTypes_ID);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PredefinedMessagesTranslated",
                c => new
                    {
                        PredefinedMessageID = c.Int(nullable: false, identity: true),
                        LanguageID = c.Int(nullable: false),
                        Message = c.String(),
                        Messages_ID = c.Int(),
                    })
                .PrimaryKey(t => t.PredefinedMessageID)
                .ForeignKey("dbo.Language", t => t.LanguageID, cascadeDelete: true)
                .ForeignKey("dbo.Messages", t => t.Messages_ID)
                .Index(t => t.LanguageID)
                .Index(t => t.Messages_ID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmergencyID = c.Int(nullable: false),
                        EmergencyTypes_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.EmergencyTypes", t => t.EmergencyTypes_ID)
                .Index(t => t.EmergencyTypes_ID);
            
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Latitude = c.String(),
                        Longitude = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        UniqueID = c.String(nullable: false, maxLength: 128),
                        DeviceOS = c.String(),
                    })
                .PrimaryKey(t => t.UniqueID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PredefinedMessagesTranslated", "Messages_ID", "dbo.Messages");
            DropForeignKey("dbo.Messages", "EmergencyTypes_ID", "dbo.EmergencyTypes");
            DropForeignKey("dbo.PredefinedMessagesTranslated", "LanguageID", "dbo.Language");
            DropForeignKey("dbo.EmergencyTranslated", "LanguageID", "dbo.Language");
            DropForeignKey("dbo.EmergencyTranslated", "EmergencyTypes_ID", "dbo.EmergencyTypes");
            DropForeignKey("dbo.Broadcasts", "EmergencyTypes_ID", "dbo.EmergencyTypes");
            DropIndex("dbo.Messages", new[] { "EmergencyTypes_ID" });
            DropIndex("dbo.PredefinedMessagesTranslated", new[] { "Messages_ID" });
            DropIndex("dbo.PredefinedMessagesTranslated", new[] { "LanguageID" });
            DropIndex("dbo.EmergencyTranslated", new[] { "EmergencyTypes_ID" });
            DropIndex("dbo.EmergencyTranslated", new[] { "LanguageID" });
            DropIndex("dbo.Broadcasts", new[] { "EmergencyTypes_ID" });
            DropTable("dbo.Devices");
            DropTable("dbo.Buildings");
            DropTable("dbo.Messages");
            DropTable("dbo.PredefinedMessagesTranslated");
            DropTable("dbo.Language");
            DropTable("dbo.EmergencyTranslated");
            DropTable("dbo.EmergencyTypes");
            DropTable("dbo.Broadcasts");
        }
    }
}
