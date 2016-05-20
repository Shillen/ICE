namespace ICE_Server.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BroadcastBuilding",
                c => new
                    {
                        BroadcastID = c.Int(nullable: false),
                        BuildingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BroadcastID, t.BuildingID })
                .ForeignKey("dbo.Broadcast", t => t.BroadcastID, cascadeDelete: true)
                .ForeignKey("dbo.Building", t => t.BuildingID, cascadeDelete: true)
                .Index(t => t.BroadcastID)
                .Index(t => t.BuildingID);
            
            CreateTable(
                "dbo.Broadcast",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Time = c.DateTime(nullable: false),
                        EmergencyId = c.Int(nullable: false),
                        PredefinedMessageID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Emergency", t => t.EmergencyId, cascadeDelete: true)
                .ForeignKey("dbo.PredefinedMessage", t => t.PredefinedMessageID)
                .Index(t => t.EmergencyId)
                .Index(t => t.PredefinedMessageID);
            
            CreateTable(
                "dbo.Emergency",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
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
                "dbo.Building",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Device",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DeviceID = c.String(maxLength: 200),
                        DeviceOS = c.Int(nullable: false),
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
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: false)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Option = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Email = c.String(nullable: false, maxLength: 256),
                        RoleId = c.Int(nullable: false),
                        Ip = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.PredefinedMessageTranslated", "PredefinedMessageID", "dbo.PredefinedMessage");
            DropForeignKey("dbo.PredefinedMessageTranslated", "LanguageID", "dbo.Language");
            DropForeignKey("dbo.EmergencyTranslated", "LanguageID", "dbo.Language");
            DropForeignKey("dbo.EmergencyTranslated", "EmergencyID", "dbo.Emergency");
            DropForeignKey("dbo.BroadcastBuilding", "BuildingID", "dbo.Building");
            DropForeignKey("dbo.BroadcastBuilding", "BroadcastID", "dbo.Broadcast");
            DropForeignKey("dbo.Broadcast", "PredefinedMessageID", "dbo.PredefinedMessage");
            DropForeignKey("dbo.PredefinedMessage", "EmergencyID", "dbo.Emergency");
            DropForeignKey("dbo.Broadcast", "EmergencyId", "dbo.Emergency");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "RoleId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PredefinedMessageTranslated", new[] { "LanguageID" });
            DropIndex("dbo.PredefinedMessageTranslated", new[] { "PredefinedMessageID" });
            DropIndex("dbo.EmergencyTranslated", new[] { "LanguageID" });
            DropIndex("dbo.EmergencyTranslated", new[] { "EmergencyID" });
            DropIndex("dbo.PredefinedMessage", new[] { "EmergencyID" });
            DropIndex("dbo.Broadcast", new[] { "PredefinedMessageID" });
            DropIndex("dbo.Broadcast", new[] { "EmergencyId" });
            DropIndex("dbo.BroadcastBuilding", new[] { "BuildingID" });
            DropIndex("dbo.BroadcastBuilding", new[] { "BroadcastID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Settings");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.PredefinedMessageTranslated");
            DropTable("dbo.Language");
            DropTable("dbo.EmergencyTranslated");
            DropTable("dbo.Device");
            DropTable("dbo.Building");
            DropTable("dbo.PredefinedMessage");
            DropTable("dbo.Emergency");
            DropTable("dbo.Broadcast");
            DropTable("dbo.BroadcastBuilding");
        }
    }
}
