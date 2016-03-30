namespace ICE_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EmergencyTranslated", "EmergencyTypes_ID", "dbo.EmergencyTypes");
            DropForeignKey("dbo.EmergencyTranslated", "LanguageID", "dbo.Language");
            DropForeignKey("dbo.PredefinedMessagesTranslated", "LanguageID", "dbo.Language");
            DropForeignKey("dbo.Messages", "EmergencyTypes_ID", "dbo.EmergencyTypes");
            DropForeignKey("dbo.PredefinedMessagesTranslated", "Messages_ID", "dbo.Messages");
            DropForeignKey("dbo.Broadcasts", "EmergencyID", "dbo.EmergencyTypes");
            DropIndex("dbo.Broadcasts", new[] { "EmergencyID" });
            DropIndex("dbo.EmergencyTranslated", new[] { "LanguageID" });
            DropIndex("dbo.EmergencyTranslated", new[] { "EmergencyTypes_ID" });
            DropIndex("dbo.PredefinedMessagesTranslated", new[] { "LanguageID" });
            DropIndex("dbo.PredefinedMessagesTranslated", new[] { "Messages_ID" });
            DropIndex("dbo.Messages", new[] { "EmergencyTypes_ID" });
            DropTable("dbo.Broadcasts");
            DropTable("dbo.EmergencyTypes");
            DropTable("dbo.EmergencyTranslated");
            DropTable("dbo.PredefinedMessagesTranslated");
            DropTable("dbo.Messages");
            DropTable("dbo.Buildings");
            DropTable("dbo.Devices");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Devices",
                c => new
                    {
                        UniqueID = c.String(nullable: false, maxLength: 128),
                        DeviceOS = c.String(),
                    })
                .PrimaryKey(t => t.UniqueID);
            
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
                "dbo.Messages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmergencyID = c.Int(nullable: false),
                        EmergencyTypes_ID = c.Int(),
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
                .PrimaryKey(t => t.PredefinedMessageID);
            
            CreateTable(
                "dbo.EmergencyTranslated",
                c => new
                    {
                        EmergencyID = c.Int(nullable: false, identity: true),
                        LanguageID = c.Int(nullable: false),
                        Name = c.String(),
                        EmergencyTypes_ID = c.Int(),
                    })
                .PrimaryKey(t => t.EmergencyID);
            
            CreateTable(
                "dbo.EmergencyTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Broadcasts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Buildings = c.String(),
                        Time = c.DateTime(nullable: false),
                        EmergencyID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.Messages", "EmergencyTypes_ID");
            CreateIndex("dbo.PredefinedMessagesTranslated", "Messages_ID");
            CreateIndex("dbo.PredefinedMessagesTranslated", "LanguageID");
            CreateIndex("dbo.EmergencyTranslated", "EmergencyTypes_ID");
            CreateIndex("dbo.EmergencyTranslated", "LanguageID");
            CreateIndex("dbo.Broadcasts", "EmergencyID");
            AddForeignKey("dbo.Broadcasts", "EmergencyID", "dbo.EmergencyTypes", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PredefinedMessagesTranslated", "Messages_ID", "dbo.Messages", "ID");
            AddForeignKey("dbo.Messages", "EmergencyTypes_ID", "dbo.EmergencyTypes", "ID");
            AddForeignKey("dbo.PredefinedMessagesTranslated", "LanguageID", "dbo.Language", "ID", cascadeDelete: true);
            AddForeignKey("dbo.EmergencyTranslated", "LanguageID", "dbo.Language", "ID", cascadeDelete: true);
            AddForeignKey("dbo.EmergencyTranslated", "EmergencyTypes_ID", "dbo.EmergencyTypes", "ID");
        }
    }
}
