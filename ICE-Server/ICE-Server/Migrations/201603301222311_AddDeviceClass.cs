namespace ICE_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeviceClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Device",
                c => new
                    {
                        UniqueID = c.String(nullable: false, maxLength: 128),
                        DeviceOS = c.String(),
                    })
                .PrimaryKey(t => t.UniqueID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Device");
        }
    }
}
