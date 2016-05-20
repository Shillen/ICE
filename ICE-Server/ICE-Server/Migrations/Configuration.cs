namespace ICE_Server.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ICE_Server.DAL.ICEContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ICE_Server.DAL.ICEContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Database.Delete();
            context.Database.Create();
            // Add some buildings
            context.Buildings.Add(new Models.Building { Name = "A" });
            context.Buildings.Add(new Models.Building { Name = "B" });
            context.Buildings.Add(new Models.Building { Name = "C" });
            context.Buildings.Add(new Models.Building { Name = "D" });
            context.Buildings.Add(new Models.Building { Name = "E" });
            context.Buildings.Add(new Models.Building { Name = "F" });
            context.Buildings.Add(new Models.Building { Name = "G" });
            context.Buildings.Add(new Models.Building { Name = "H" });
            context.Buildings.Add(new Models.Building { Name = "T" });
            context.Buildings.Add(new Models.Building { Name = "X" });

            // Add some devices
            context.Devices.Add(new Models.Device { DeviceID = "APA91bECIBtIhVHGC_VoN4DZiC6SekEucXTwbLtpcVQGnaij_X0eDPN9jBaNVzv_uNRR3SDbREKxdt9DQhDhbfh1IQp9J4byJq7nCPiC-1i3NgsaKXUGhP6c7m3GDfRrbKVY0WBn3CuyewzLw5jSwj0smZd3lWGXjA", DeviceOS = Models.OS.Android });
            context.Devices.Add(new Models.Device { DeviceID = "APA91sECIBtIhVHGC_VoN4DZiC6SekEucXTwbLtpcVQGnaij_X0eDPN9jBaNVzv_uNRR3SDbREKxdt9DQhDhbfh1IQp9J4byJq7nCPiC-1i3NgsaKXUGhP6c7m3GDfRrbKVY0WBn3CuyewzLw5jSwj0smZd3lWGXjA", DeviceOS = Models.OS.Android });

            //Add some languages
            context.Languages.Add(new Models.Language { ID = 1, Code = "EN" });
            context.Languages.Add(new Models.Language { ID = 2, Code = "NL" });

            // Add some EmergencyTyes
            context.Emergency.Add(new Models.Emergency { ID = 1, Name = "Fire" });
            context.Emergency.Add(new Models.Emergency { ID = 2, Name = "Shooting" });
            context.Emergency.Add(new Models.Emergency { ID = 3, Name = "Earthquake" });

            // Add some translations
            context.EmergencyTranslated.Add(new Models.EmergencyTranslated { EmergencyID = 1, LanguageID = 1, Name = "Fire" });
            context.EmergencyTranslated.Add(new Models.EmergencyTranslated { EmergencyID = 2, LanguageID = 1, Name = "Shooting" });
            context.EmergencyTranslated.Add(new Models.EmergencyTranslated { EmergencyID = 3, LanguageID = 1, Name = "Gas leak" });

            context.EmergencyTranslated.Add(new Models.EmergencyTranslated { EmergencyID = 1, LanguageID = 2, Name = "Vuur" });
            context.EmergencyTranslated.Add(new Models.EmergencyTranslated { EmergencyID = 2, LanguageID = 2, Name = "Schiet partij" });
            context.EmergencyTranslated.Add(new Models.EmergencyTranslated { EmergencyID = 3, LanguageID = 2, Name = "Gaslek" });

            // Add some messages
            context.PredefinedMessages.Add(new Models.PredefinedMessage { ID = 1, EmergencyID = 1 });
            context.PredefinedMessages.Add(new Models.PredefinedMessage { ID = 2, EmergencyID = 2 });
            context.PredefinedMessages.Add(new Models.PredefinedMessage { ID = 3, EmergencyID = 3 });

            // Add some translations
            context.PredefinedMessagesTranslated.Add(new Models.PredefinedMessageTranslated { PredefinedMessageID = 1, LanguageID = 1, Message = "There is a fire!" });
            context.PredefinedMessagesTranslated.Add(new Models.PredefinedMessageTranslated { PredefinedMessageID = 1, LanguageID = 2, Message = "Er is een brand!" });

            context.Roles.AddOrUpdate(
                r => r.Id,
                new Role
                {
                    Id = 1,
                    Name = "Admin"
                },
                new Role
                {
                    Id = 2,
                    Name = "User"
                }
            );

            context.Users.AddOrUpdate(
                    u => u.Id,
                    new User
                    {
                        Id = 1,
                        Email = "admin@windesheim.nl",
                        EmailConfirmed = true,
                        PasswordHash = "ABRYILynS2HeMz2PeFpd/a5K/6WnUaioZGbzqGdjL3qflAusqNuK1KmtAlCc4hXX6Q==",
                        SecurityStamp = "40797fad-6af5-4651-8872-2990568d14bc",
                        RoleId = 1,
                        UserName = "Admin"
                    },
                    new User
                    {
                        Id = 2,
                        Email = "michael@windesheim.nl",
                        EmailConfirmed = true,
                        PasswordHash = "ABRYILynS2HeMz2PeFpd/a5K/6WnUaioZGbzqGdjL3qflAusqNuK1KmtAlCc4hXX6Q==",
                        SecurityStamp = "40797fad-6af5-4651-8872-2990568d14bc",
                        RoleId = 2,
                        UserName = "Michael"
                    }
                );
            // Add some broadcasts
            //context.Broadcasts.Add(new Models.Broadcast { Message = "There is a fire!", Buildings = new List<Models.Building>() { new Models.Building() { ID = 1, Name = "A" } }, EmergencyId = 1 });
            //context.Broadcasts.Add(new Models.Broadcast { Message = "There is a shooting", Buildings = new List<Models.Building>() { new Models.Building() { ID = 2, Name = "B" } }, EmergencyId = 2 });


        }
    }
}
