using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Task_Entities.Entities;

namespace Task_DAL.Data;

public partial class DbTaskContext : DbContext
{
    public DbTaskContext()
    {
    }

    public DbTaskContext(DbContextOptions<DbTaskContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=  DESKTOP-SIIB9G2\\SQLEXPRESS;Database=DbTask;Trusted_Connection=True;encrypt=false");

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
