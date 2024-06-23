using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using CompanyApp.Models.Entity;

namespace CompanyApp.Data;

public class CompanyDbContext : DbContext
{
    public CompanyDbContext()
    {
    }

    public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductDetail> ProductDetails { get; set; }
    public virtual DbSet<Supplier> Suppliers { get; set; }
    public virtual DbSet<Size> Sizes { get; set; }
    public virtual DbSet<ProductStock> ProductStocks { get; set; }
    public virtual DbSet<Receipt> Receipts { get; set; }
    public virtual DbSet<ReceiptDetail> ReceiptDetails { get; set; }
    public virtual DbSet<RollNumber> RollNumbers { get; set; }
    public virtual DbSet<Issue> Issues { get; set; }
    public virtual DbSet<ProductionCoating> ProductionCoatings { get; set; }
    public virtual DbSet<ProductionCalendaring> ProductionCalendarings { get; set; }
    public virtual DbSet<ProductionSlitting> ProductionSlittings { get; set; }
    public virtual DbSet<SlittingDetail> SlittingDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the one-to-many relationship
        modelBuilder.Entity<ProductDetail>()
            .HasOne(_ => _.Products)                   // Each productdetail has one product
            .WithMany(_ => _.ProductDetails)           // Each product has many productdetails
            .HasForeignKey(_ => _.ProductId)           // The foreign key is ProductId
            .OnDelete(DeleteBehavior.Cascade);         // Configure cascade delete

        modelBuilder.Entity<ProductStock>()
            .HasOne(_ => _.ProductDetails)
            .WithMany(_ => _.ProductStocks)
            .HasForeignKey(_ => _.ProductDetailId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProductStock>()
            .HasOne(_ => _.Sizes)
            .WithMany(_ => _.ProductStocks)
            .HasForeignKey(_ => _.SizeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Receipt>()
            .HasOne(_ => _.Suppliers)
            .WithMany(_ => _.Receipts)
            .HasForeignKey(_ => _.SupplierId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ReceiptDetail>()
            .HasOne(_ => _.Receipts)
            .WithMany(_ => _.ReceiptDetails)
            .HasForeignKey(_ => _.ReceiptId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ReceiptDetail>()
            .HasOne(_ => _.ProductStocks)
            .WithMany(_ => _.ReceiptDetails)
            .HasForeignKey(_ => _.ProductStockId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RollNumber>()
            .HasOne(_ => _.ReceiptDetails)
            .WithMany(_ => _.RollNumbers)
            .HasForeignKey(_ => _.ReceiptDetailId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Issue>()
            .HasOne(_ => _.RollNumbers)
            .WithMany(_ => _.Issues)
            .HasForeignKey(_ => _.RollNumberId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Issue>()
            .HasOne(_ => _.ProductStocks)
            .WithMany(_ => _.Issues)
            .HasForeignKey(_ => _.ProductStockId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ProductionCoating>()
            .HasOne(_ => _.Issues)
            .WithMany(_ => _.ProductionCoatings)
            .HasForeignKey(_ => _.IssueId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProductionCalendaring>()
            .HasOne(_ => _.ProductionCoatings)
            .WithMany(_ => _.ProductionCalendarings)
            .HasForeignKey(_ => _.ProductionCoatingId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProductionSlitting>()
            .HasOne(_ => _.ProductionCalendarings)
            .WithMany(_ => _.ProductionSlittings)
            .HasForeignKey(_ => _.ProductionCalendaringId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<SlittingDetail>()
            .HasOne(_ => _.ProductionSlittings)
            .WithMany(_ => _.SlittingDetails)
            .HasForeignKey(_ => _.ProductionSlittingId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Receipt>()
            .Property(e => e.ReceiptDate)
            .HasConversion(DateOnlyToDateTimeConverter);

        modelBuilder.Entity<Receipt>()
            .Property(e => e.BillDate)
            .HasConversion(DateOnlyToDateTimeConverter);

        modelBuilder.Entity<Issue>()
            .Property(e => e.IssueDate)
            .HasConversion(DateOnlyToDateTimeConverter);

        modelBuilder.Entity<ProductionCoating>()
            .Property(e => e.ProductionCoatingDate)
            .HasConversion(DateOnlyToDateTimeConverter);

        modelBuilder.Entity<ProductionCoating>()
            .Property(e => e.CoatingStart)
            .HasConversion(TimeOnlyToTimeSpanConverter);

        modelBuilder.Entity<ProductionCoating>()
            .Property(e => e.CoatingEnd)
            .HasConversion(TimeOnlyToTimeSpanConverter);

        modelBuilder.Entity<ProductionCalendaring>()
            .Property(e => e.ProductionCoatingDate)
            .HasConversion(DateOnlyToDateTimeConverter);

        modelBuilder.Entity<ProductionCalendaring>()
            .Property(e => e.CalendaringStart)
            .HasConversion(TimeOnlyToTimeSpanConverter);

        modelBuilder.Entity<ProductionCalendaring>()
            .Property(e => e.CalendaringEnd)
            .HasConversion(TimeOnlyToTimeSpanConverter);

        modelBuilder.Entity<ProductionSlitting>()
            .Property(e => e.ProductionCoatingDate)
            .HasConversion(DateOnlyToDateTimeConverter);

        modelBuilder.Entity<ProductionSlitting>()
            .Property(e => e.SlittingStart)
            .HasConversion(TimeOnlyToTimeSpanConverter);

        modelBuilder.Entity<ProductionSlitting>()
            .Property(e => e.SlittingEnd)
            .HasConversion(TimeOnlyToTimeSpanConverter);
    }
}
