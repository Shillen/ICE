//using System.Data.Entity;
 
//namespace ICE_Server.DAL
//{
//    public class ICEInitialize : DropCreateDatabaseIfModelChanges<ICEContext>
//    {

//        protected override void Seed(ICEContext context)
//        {
//            // Add some buildings
//            context.Buildings.Add(new Models.Buildings { Name = "A" });
//            context.Buildings.Add(new Models.Buildings { Name = "B" });
//            context.Buildings.Add(new Models.Buildings { Name = "C" });
//            context.Buildings.Add(new Models.Buildings { Name = "D" });
//            context.Buildings.Add(new Models.Buildings { Name = "E" });
//            context.Buildings.Add(new Models.Buildings { Name = "F" });
//            context.Buildings.Add(new Models.Buildings { Name = "G" });
//            context.Buildings.Add(new Models.Buildings { Name = "H" });
//            context.Buildings.Add(new Models.Buildings { Name = "S" });
//            context.Buildings.Add(new Models.Buildings { Name = "X" });

//            // Add some devices
//            context.Devices.Add(new Models.Devices { UniqueID = "APA91bECIBtIhVHGC_VoN4DZiC6SekEucXTwbLtpcVQGnaij_X0eDPN9jBaNVzv_uNRR3SDbREKxdt9DQhDhbfh1IQp9J4byJq7nCPiC-1i3NgsaKXUGhP6c7m3GDfRrbKVY0WBn3CuyewzLw5jSwj0smZd3lWGXjA", DeviceOS = "Android" });
//            context.Devices.Add(new Models.Devices { UniqueID = "APA91sECIBtIhVHGC_VoN4DZiC6SekEucXTwbLtpcVQGnaij_X0eDPN9jBaNVzv_uNRR3SDbREKxdt9DQhDhbfh1IQp9J4byJq7nCPiC-1i3NgsaKXUGhP6c7m3GDfRrbKVY0WBn3CuyewzLw5jSwj0smZd3lWGXjA", DeviceOS = "Android" });

//            // Add some languages
//            context.Language.Add(new Models.Language { ID = 1 });

//            // Add some EmergencyTyes
//            context.EmergencyTypes.Add(new Models.EmergencyTypes { ID = 1 });
//            context.EmergencyTypes.Add(new Models.EmergencyTypes { ID = 2 });
//            context.EmergencyTypes.Add(new Models.EmergencyTypes { ID = 3 });


//            // Add some translations
//            context.EmergencyTranslated.Add(new Models.EmergencyTranslated { EmergencyID = 1, LanguageID = 1, Name = "Fire" });

//            context.SaveChanges();

//            base.Seed(context);

//        }
//    }
//}