using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ICE_Server.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ICE_Server.DAL
{
    public class ICEContext : DbContext
    {

        public ICEContext() : base("ICEContext")
        {
        }

        

        public DbSet<Broadcasts> Broadcasts { get; set; }
        public DbSet<Buildings> Buildings { get; set; }
        public DbSet<Devices> Devices { get; set; }
        public DbSet<EmergencyTypes> EmergencyTypes { get; set; }
        public DbSet<Messages> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}