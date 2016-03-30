namespace ICE_Server.Migrations
{
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
            context.Database.Delete();
            context.Database.Create();
            // Add some buildings
            //context.Buildings.Add(new Models.Buildings { Name = "A" });
            //context.Buildings.Add(new Models.Buildings { Name = "B" });
            //context.Buildings.Add(new Models.Buildings { Name = "C" });
            //context.Buildings.Add(new Models.Buildings { Name = "D" });
            //context.Buildings.Add(new Models.Buildings { Name = "E" });
            //context.Buildings.Add(new Models.Buildings { Name = "F" });
            //context.Buildings.Add(new Models.Buildings { Name = "G" });
            //context.Buildings.Add(new Models.Buildings { Name = "H" });
            //context.Buildings.Add(new Models.Buildings { Name = "S" });
            //context.Buildings.Add(new Models.Buildings { Name = "X" });

            //// Add some devices
            //context.Devices.Add(new Models.Devices { UniqueID = "APA91bECIBtIhVHGC_VoN4DZiC6SekEucXTwbLtpcVQGnaij_X0eDPN9jBaNVzv_uNRR3SDbREKxdt9DQhDhbfh1IQp9J4byJq7nCPiC-1i3NgsaKXUGhP6c7m3GDfRrbKVY0WBn3CuyewzLw5jSwj0smZd3lWGXjA", DeviceOS = "Android" });
            //context.Devices.Add(new Models.Devices { UniqueID = "APA91sECIBtIhVHGC_VoN4DZiC6SekEucXTwbLtpcVQGnaij_X0eDPN9jBaNVzv_uNRR3SDbREKxdt9DQhDhbfh1IQp9J4byJq7nCPiC-1i3NgsaKXUGhP6c7m3GDfRrbKVY0WBn3CuyewzLw5jSwj0smZd3lWGXjA", DeviceOS = "Android" });

            // Add some languages
            context.Languages.Add(new Models.Language { ID = 1, Code = "EN" });
            context.Languages.Add(new Models.Language { ID = 2, Code = "NL" });

            //// Add some EmergencyTyes
            //context.EmergencyTypes.Add(new Models.EmergencyTypes { ID = 1 });
            //context.EmergencyTypes.Add(new Models.EmergencyTypes { ID = 2 });
            //context.EmergencyTypes.Add(new Models.EmergencyTypes { ID = 3 });


            //// Add some translations
            //context.EmergencyTranslated.Add(new Models.EmergencyTranslated { EmergencyID = 1, LanguageID = 1, Name = "Fire" });
            //context.EmergencyTranslated.Add(new Models.EmergencyTranslated { EmergencyID = 2, LanguageID = 1, Name = "Shooting" });
            //context.EmergencyTranslated.Add(new Models.EmergencyTranslated { EmergencyID = 3, LanguageID = 1, Name = "Gas leak" });

            //context.EmergencyTranslated.Add(new Models.EmergencyTranslated { EmergencyID = 1, LanguageID = 2, Name = "Vuur" });
            //context.EmergencyTranslated.Add(new Models.EmergencyTranslated { EmergencyID = 2, LanguageID = 2, Name = "Schiet partij" });
            //context.EmergencyTranslated.Add(new Models.EmergencyTranslated { EmergencyID = 3, LanguageID = 2, Name = "Gaslek" });

            //// Add some messages
            //context.Messages.Add(new Models.Messages { ID = 1, EmergencyID = 1});
            //context.Messages.Add(new Models.Messages { ID = 2, EmergencyID = 2 });
            //context.Messages.Add(new Models.Messages { ID = 3, EmergencyID = 3 });

            //// Add some translations
            //context.PredefinedMessagesTranslated.Add(new Models.PredefinedMessagesTranslated { PredefinedMessageID = 1, LanguageID = 1, Message = "There is a fire!" });
            //context.PredefinedMessagesTranslated.Add(new Models.PredefinedMessagesTranslated { PredefinedMessageID = 1, LanguageID = 2, Message = "Er is een brand!" });

            //// Add some broadcasts
            //context.Broadcasts.Add(new Models.Broadcasts { Message = "There is a fire!", Buildings = "X", EmergencyID = 1 });
            //context.Broadcasts.Add(new Models.Broadcasts { Message = "There is a shooting", Buildings = "X", EmergencyID = 2 });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
