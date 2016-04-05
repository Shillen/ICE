namespace ICE_Server.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;
    internal sealed class Configuration : DbMigrationsConfiguration<ICE_Server.DAL.ICEContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// Wrapper for SaveChanges adding the Validation Messages to the generated exception
        /// </summary>
        /// <param name="context">The context.</param>
        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }

        protected override void Seed(ICE_Server.DAL.ICEContext context)
        {
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
            context.Buildings.Add(new Models.Building { Name = "S" });
            context.Buildings.Add(new Models.Building { Name = "X" });

            // Add some devices
            context.Devices.Add(new Models.Device { UniqueID = "APA91bECIBtIhVHGC_VoN4DZiC6SekEucXTwbLtpcVQGnaij_X0eDPN9jBaNVzv_uNRR3SDbREKxdt9DQhDhbfh1IQp9J4byJq7nCPiC-1i3NgsaKXUGhP6c7m3GDfRrbKVY0WBn3CuyewzLw5jSwj0smZd3lWGXjA", DeviceOS = "Android" });
            context.Devices.Add(new Models.Device { UniqueID = "APA91sECIBtIhVHGC_VoN4DZiC6SekEucXTwbLtpcVQGnaij_X0eDPN9jBaNVzv_uNRR3SDbREKxdt9DQhDhbfh1IQp9J4byJq7nCPiC-1i3NgsaKXUGhP6c7m3GDfRrbKVY0WBn3CuyewzLw5jSwj0smZd3lWGXjA", DeviceOS = "Android" });

            //Add some languages
            context.Languages.Add(new Models.Language { ID = 1, Code = "EN" });
            context.Languages.Add(new Models.Language { ID = 2, Code = "NL" });

            // Add some EmergencyTyes
            context.Emergencies.Add(new Models.Emergency { ID = 1 });
            context.Emergencies.Add(new Models.Emergency { ID = 2 });
            context.Emergencies.Add(new Models.Emergency { ID = 3 });


            // Add some translations
            context.EmergenciesTranslated.Add(new Models.EmergencyTranslated { EmergencyID = 1, LanguageID = 1, Name = "Fire" });
            context.EmergenciesTranslated.Add(new Models.EmergencyTranslated { EmergencyID = 2, LanguageID = 1, Name = "Shooting" });
            context.EmergenciesTranslated.Add(new Models.EmergencyTranslated { EmergencyID = 3, LanguageID = 1, Name = "Gas leak" });

            context.EmergenciesTranslated.Add(new Models.EmergencyTranslated { EmergencyID = 1, LanguageID = 2, Name = "Vuur" });
            context.EmergenciesTranslated.Add(new Models.EmergencyTranslated { EmergencyID = 2, LanguageID = 2, Name = "Schiet partij" });
            context.EmergenciesTranslated.Add(new Models.EmergencyTranslated { EmergencyID = 3, LanguageID = 2, Name = "Gaslek" });

            // Add some messages
            context.PredefinedMessages.Add(new Models.PredefinedMessage { ID = 1, EmergencyID = 1 });
            context.PredefinedMessages.Add(new Models.PredefinedMessage { ID = 2, EmergencyID = 2 });
            context.PredefinedMessages.Add(new Models.PredefinedMessage { ID = 3, EmergencyID = 3 });

            // Add some translations
            context.PredefinedMessagesTranslated.Add(new Models.PredefinedMessageTranslated { PredefinedMessageID = 1, LanguageID = 1, Message = "There is a fire!" });
            context.PredefinedMessagesTranslated.Add(new Models.PredefinedMessageTranslated { PredefinedMessageID = 1, LanguageID = 2, Message = "Er is een brand!" });


            // Add some broadcasts
            //context.Broadcasts.Add(new Models.Broadcast { Message = "There is a fire!", Buildings = new List<Models.Building>() { new Models.Building() { ID = 1, Name = "A" } }, EmergencyId = 1 });
            //context.Broadcasts.Add(new Models.Broadcast { Message = "There is a shooting", Buildings = new List<Models.Building>() { new Models.Building() { ID = 2, Name = "B" } }, EmergencyId = 2 });

            SaveChanges(context);

            base.Seed(context);

        }

    }
}
