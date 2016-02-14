using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ERPDataStaging.Models;


namespace ERPDataStaging.DAL
{
    public class ERPDataStagingDBContext : DbContext
    {
        public ERPDataStagingDBContext() 
            : base(@"data source=(LocalDb)\MSSQLLocalDB; 
                 initial catalog=ERPDataStagingDB;
                 integrated security=true")
        {
            ////Disable initializer
            //Database.SetInitializer<ERPDataStagingDBContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Product>().ToTable("Product");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }

    }
}