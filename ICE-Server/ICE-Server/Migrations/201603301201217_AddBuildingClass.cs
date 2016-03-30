namespace ICE_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBuildingClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Building",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Building");
        }
    }
}
