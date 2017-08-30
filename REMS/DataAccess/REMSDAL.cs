using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using REMS.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace REMS.DataAccess
{
    public class REMSDAL : DbContext
    {
        public DbSet<Complex> Complexes { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<ContactInfo> Contacts { get; set; }
        public DbSet<StaffMember> StaffMembers { get; set; }
        public DbSet<Owner> Owners { get; set; }

        public REMSDAL(): base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<REMSDAL, REMS.Migrations.Configuration>("DefaultConnection"));
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Complex>().ToTable("Complex");
            //modelBuilder.Entity<Unit>().ToTable("Unit");
            //modelBuilder.Entity<Address>().ToTable("Addresses");
            //modelBuilder.Entity<Tenant>().ToTable("Tenant");

            modelBuilder.Entity<ContactInfo>().ToTable("Contacts");

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Complex>().HasOptional(x => x.Owners);
            modelBuilder.Entity<Owner>().HasOptional(x => x.Complexes);
            modelBuilder.Entity<Unit>().HasOptional(x => x.Complex).WithMany(x => x.Units);
            modelBuilder.Entity<Complex>().HasMany(x => x.Owners);
            modelBuilder.Entity<Owner>().HasMany(x => x.Complexes);

            base.OnModelCreating(modelBuilder);
        }
    }
}