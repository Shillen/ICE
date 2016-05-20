using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ICE_Server.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ICE_Server.DAL
{
    public class ICEContext : IdentityDbContext<User, Role, int, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public ICEContext() : base("name=ICEContext")
        {
        }

        public static ICEContext Create()
        {
            return new ICEContext();
        }

        public virtual DbSet<Broadcast> Broadcast { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<BroadcastBuilding> BroadcastBuilding { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<Emergency> Emergency { get; set; }
        public virtual DbSet<EmergencyTranslated> EmergencyTranslated { get; set; }
        public virtual DbSet<PredefinedMessage> PredefinedMessages { get; set; }
        public virtual DbSet<PredefinedMessageTranslated> PredefinedMessagesTranslated { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }

        //public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Emergency>().ToTable("Emergency");
            modelBuilder.Entity<EmergencyTranslated>().ToTable("EmergencyTranslated");
            //modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            //modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            //modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            //modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}