namespace ICE_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Broadcast", "PredefinedMessageID", c => c.Int());
            AddColumn("dbo.Emergency", "Name", c => c.String());
            AlterColumn("dbo.Device", "DeviceOS", c => c.Int(nullable: false));
            CreateIndex("dbo.Broadcast", "PredefinedMessageID");
            AddForeignKey("dbo.Broadcast", "PredefinedMessageID", "dbo.PredefinedMessage", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Broadcast", "PredefinedMessageID", "dbo.PredefinedMessage");
            DropIndex("dbo.Broadcast", new[] { "PredefinedMessageID" });
            AlterColumn("dbo.Device", "DeviceOS", c => c.String());
            DropColumn("dbo.Emergency", "Name");
            DropColumn("dbo.Broadcast", "PredefinedMessageID");
        }
    }
}
