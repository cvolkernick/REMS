using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using REMS.Models;

namespace REMS.DataAccess
{
    public class REMSDAL : DbContext
    {
        public DbSet<Property> Properties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>().ToTable("Property");
            base.OnModelCreating(modelBuilder);
        }
    }
}