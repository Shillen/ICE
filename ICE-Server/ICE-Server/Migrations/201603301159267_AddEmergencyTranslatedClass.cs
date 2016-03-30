namespace ICE_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmergencyTranslatedClass : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmergencyTranslated", "LanguageID", "dbo.Language");
            DropForeignKey("dbo.EmergencyTranslated", "EmergencyID", "dbo.Emergency");
            DropIndex("dbo.EmergencyTranslated", new[] { "LanguageID" });
            DropIndex("dbo.EmergencyTranslated", new[] { "EmergencyID" });
            DropTable("dbo.EmergencyTranslated");
        }
    }
}
