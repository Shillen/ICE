﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ICE_Server.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ICEEntities : DbContext
    {
        public ICEEntities()
            : base("name=ICEEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Broadcasts> Broadcasts { get; set; }
        public virtual DbSet<Buildings> Buildings { get; set; }
        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<EmergencyTypes> EmergencyTypes { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
