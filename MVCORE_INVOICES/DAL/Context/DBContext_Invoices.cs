using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using DAL.Entities;

namespace DAL.Context
{
    public partial class DBContext_Invoices : DbContext
    {

        public DBContext_Invoices()
        {
        }

        public DBContext_Invoices(DbContextOptions<DBContext_Invoices> options)
            : base(options)
        {
        }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=DESKTOP-SIIB9G2\\SQLEXPRESS;Database=Invoices;Trusted_Connection=True;encrypt=false");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(d => d.Invoice).WithMany(p => p.Products).HasConstraintName("FK_Product_Invoices");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}
