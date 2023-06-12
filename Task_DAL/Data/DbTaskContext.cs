 
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
        => optionsBuilder.UseSqlServer("Server=DESKTOP-SIIB9G2\\SQLEXPRESS;Database=DbTask;Trusted_Connection=True;encrypt=false");

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
