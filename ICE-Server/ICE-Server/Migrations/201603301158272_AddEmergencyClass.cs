namespace ICE_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmergencyClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emergency",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Emergency");
        }
    }
}
