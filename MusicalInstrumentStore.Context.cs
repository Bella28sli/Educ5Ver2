﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Educ5Ver2
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MusicalInstrumentStoreEntities : DbContext
    {
        public MusicalInstrumentStoreEntities()
            : base("name=MusicalInstrumentStoreEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Checks> Checks { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Makers> Makers { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Pickup_Points> Pickup_Points { get; set; }
        public virtual DbSet<Positions> Positions { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Providers> Providers { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<Staff_Choice> Staff_Choice { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
