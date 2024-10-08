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


    public virtual DbSet<User> Users { get; set; }

    
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

        modelBuilder.Entity<ProductStock>()
            .Property(_ => _.SizeId)
            .IsRequired(false); // Ensure it's optional

        modelBuilder.Entity<Issue>()
            .Property(_ => _.RollNumberId)
            .IsRequired(false); // Ensure it's optional

        // Define Value Converter for DateOnly to DateTime
        var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
            d => d.ToDateTime(TimeOnly.MinValue),      // Convert DateOnly to DateTime for storage
            d => DateOnly.FromDateTime(d)              // Convert DateTime back to DateOnly
        );

        // Define Value Converter for TimeOnly to TimeSpan
        var timeOnlyConverter = new ValueConverter<TimeOnly, TimeSpan>(
            t => t.ToTimeSpan(),                       // Convert TimeOnly to TimeSpan for storage
            t => TimeOnly.FromTimeSpan(t)              // Convert TimeSpan back to TimeOnly
        );

        modelBuilder.Entity<Receipt>()
            .Property(e => e.ReceiptDate)
            .HasConversion(dateOnlyConverter)
            .HasColumnType("date");

        modelBuilder.Entity<Receipt>()
            .Property(e => e.BillDate)
            .HasConversion(dateOnlyConverter)
            .HasColumnType("date");;

        modelBuilder.Entity<Issue>()
            .Property(e => e.IssueDate)
            .HasConversion(dateOnlyConverter)
            .HasColumnType("date");;

        modelBuilder.Entity<ProductionCoating>()
            .Property(e => e.ProductionCoatingDate)
            .HasConversion(dateOnlyConverter)
            .HasColumnType("date");;

        modelBuilder.Entity<ProductionCoating>()
            .Property(e => e.CoatingStart)
            .HasConversion(timeOnlyConverter)
            .HasColumnType("time");

        modelBuilder.Entity<ProductionCoating>()
            .Property(e => e.CoatingEnd)
            .HasConversion(timeOnlyConverter)
            .HasColumnType("time");

        modelBuilder.Entity<ProductionCalendaring>()
            .Property(e => e.ProductionCoatingDate)
            .HasConversion(dateOnlyConverter)
            .HasColumnType("date");

        modelBuilder.Entity<ProductionCalendaring>()
            .Property(e => e.CalendaringStart)
            .HasConversion(timeOnlyConverter)
            .HasColumnType("time");

        modelBuilder.Entity<ProductionCalendaring>()
            .Property(e => e.CalendaringEnd)
            .HasConversion(timeOnlyConverter)
            .HasColumnType("time");

        modelBuilder.Entity<ProductionSlitting>()
            .Property(e => e.ProductionCoatingDate)
            .HasConversion(dateOnlyConverter)
            .HasColumnType("date");

        modelBuilder.Entity<ProductionSlitting>()
            .Property(e => e.SlittingStart)
            .HasConversion(timeOnlyConverter)
            .HasColumnType("time");

        modelBuilder.Entity<ProductionSlitting>()
            .Property(e => e.SlittingEnd)
            .HasConversion(timeOnlyConverter)
            .HasColumnType("time");

        
        modelBuilder.Entity<User>(user =>
        {
            user.HasIndex(x => x.Username).IsUnique();
            user.HasIndex(x => x.Email).IsUnique();
        });
    }
}
