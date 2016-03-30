namespace ICE_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPredefinedMessageTranslatedClass : DbMigration
    {
        public override void Up()
        {
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
            DropIndex("dbo.PredefinedMessageTranslated", new[] { "LanguageID" });
            DropIndex("dbo.PredefinedMessageTranslated", new[] { "PredefinedMessageID" });
            DropTable("dbo.PredefinedMessageTranslated");
        }
    }
}
