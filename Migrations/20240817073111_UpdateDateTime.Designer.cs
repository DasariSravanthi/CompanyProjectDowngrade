﻿// <auto-generated />
using System;
using CompanyApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CompanyApp.Migrations
{
    [DbContext(typeof(CompanyDbContext))]
    [Migration("20240817073111_UpdateDateTime")]
    partial class UpdateDateTime
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CompanyApp.Models.Entity.Issue", b =>
                {
                    b.Property<int>("IssueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Issue_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IssueId"));

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("date")
                        .HasColumnName("I_Date");

                    b.Property<float?>("Moisture")
                        .HasColumnType("real");

                    b.Property<short>("ProductStockId")
                        .HasColumnType("smallint")
                        .HasColumnName("PS_Id");

                    b.Property<string>("RollNumber")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Roll_No");

                    b.Property<int?>("RollNumberId")
                        .HasColumnType("int")
                        .HasColumnName("RN_Id");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("IssueId");

                    b.HasIndex("ProductStockId");

                    b.HasIndex("RollNumberId");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.Product", b =>
                {
                    b.Property<byte>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("P_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("ProductId"));

                    b.Property<string>("ProductCategory")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Product_Category");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.ProductDetail", b =>
                {
                    b.Property<byte>("ProductDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("PD_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("ProductDetailId"));

                    b.Property<byte>("ProductId")
                        .HasColumnType("tinyint")
                        .HasColumnName("P_Id");

                    b.Property<string>("Variant")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("ProductDetailId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductDetails");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.ProductStock", b =>
                {
                    b.Property<short>("ProductStockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasColumnName("PS_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<short>("ProductStockId"));

                    b.Property<byte?>("GSM")
                        .HasColumnType("tinyint");

                    b.Property<byte>("ProductDetailId")
                        .HasColumnType("tinyint")
                        .HasColumnName("PD_Id");

                    b.Property<byte?>("RollCount")
                        .HasColumnType("tinyint")
                        .HasColumnName("Roll_Count");

                    b.Property<byte?>("SizeId")
                        .HasColumnType("tinyint")
                        .HasColumnName("Size_Id");

                    b.Property<short>("WeightInKgs")
                        .HasColumnType("smallint")
                        .HasColumnName("Weight");

                    b.HasKey("ProductStockId");

                    b.HasIndex("ProductDetailId");

                    b.HasIndex("SizeId");

                    b.ToTable("ProductStocks");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.ProductionCalendaring", b =>
                {
                    b.Property<int>("ProductionCalendaringId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PC_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductionCalendaringId"));

                    b.Property<float>("BeforeMoisture")
                        .HasColumnType("real")
                        .HasColumnName("Before_Moisture");

                    b.Property<float>("BeforeWeight")
                        .HasColumnType("real")
                        .HasColumnName("Before_Weight");

                    b.Property<TimeSpan>("CalendaringEnd")
                        .HasColumnType("time")
                        .HasColumnName("Calendaring_End");

                    b.Property<TimeSpan>("CalendaringStart")
                        .HasColumnType("time")
                        .HasColumnName("Calendaring_Start");

                    b.Property<DateTime>("ProductionCoatingDate")
                        .HasColumnType("date")
                        .HasColumnName("P_Date");

                    b.Property<int>("ProductionCoatingId")
                        .HasColumnType("int")
                        .HasColumnName("P_Id");

                    b.Property<byte>("RollCount")
                        .HasColumnType("tinyint")
                        .HasColumnName("Roll_Count");

                    b.Property<string>("RollNumber")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Roll_No");

                    b.HasKey("ProductionCalendaringId");

                    b.HasIndex("ProductionCoatingId");

                    b.ToTable("ProductionCalendarings");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.ProductionCoating", b =>
                {
                    b.Property<int>("ProductionCoatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("P_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductionCoatingId"));

                    b.Property<byte>("AverageSpeed")
                        .HasColumnType("tinyint")
                        .HasColumnName("Avg_Speed");

                    b.Property<byte>("AverageTemperature")
                        .HasColumnType("tinyint")
                        .HasColumnName("Avg_Temperature");

                    b.Property<TimeSpan>("CoatingEnd")
                        .HasColumnType("time")
                        .HasColumnName("Coating_End");

                    b.Property<TimeSpan>("CoatingStart")
                        .HasColumnType("time")
                        .HasColumnName("Coating_Start");

                    b.Property<byte>("GSMCoated")
                        .HasColumnType("tinyint")
                        .HasColumnName("GSM_Coated");

                    b.Property<int>("IssueId")
                        .HasColumnType("int")
                        .HasColumnName("Issue_Id");

                    b.Property<DateTime>("ProductionCoatingDate")
                        .HasColumnType("date")
                        .HasColumnName("P_Date");

                    b.Property<byte>("RollCount")
                        .HasColumnType("tinyint")
                        .HasColumnName("Roll_Count");

                    b.HasKey("ProductionCoatingId");

                    b.HasIndex("IssueId");

                    b.ToTable("ProductionCoatings");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.ProductionSlitting", b =>
                {
                    b.Property<int>("ProductionSlittingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PS_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductionSlittingId"));

                    b.Property<float>("BeforeMoisture")
                        .HasColumnType("real")
                        .HasColumnName("Before_Moisture");

                    b.Property<float>("BeforeWeight")
                        .HasColumnType("real")
                        .HasColumnName("Before_Weight");

                    b.Property<int>("ProductionCalendaringId")
                        .HasColumnType("int")
                        .HasColumnName("PC_Id");

                    b.Property<DateTime>("ProductionCoatingDate")
                        .HasColumnType("date")
                        .HasColumnName("P_Date");

                    b.Property<byte>("RollCount")
                        .HasColumnType("tinyint")
                        .HasColumnName("Roll_Count");

                    b.Property<string>("RollNumber")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Roll_No");

                    b.Property<TimeSpan>("SlittingEnd")
                        .HasColumnType("time")
                        .HasColumnName("Slitting_End");

                    b.Property<TimeSpan>("SlittingStart")
                        .HasColumnType("time")
                        .HasColumnName("Slitting_Start");

                    b.HasKey("ProductionSlittingId");

                    b.HasIndex("ProductionCalendaringId");

                    b.ToTable("ProductionSlittings");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.Receipt", b =>
                {
                    b.Property<int>("ReceiptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("R_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReceiptId"));

                    b.Property<DateTime>("BillDate")
                        .HasColumnType("date")
                        .HasColumnName("Bill_Date");

                    b.Property<string>("BillNo")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Bill_No");

                    b.Property<double>("BillValue")
                        .HasColumnType("float")
                        .HasColumnName("Bill_Value");

                    b.Property<DateTime>("ReceiptDate")
                        .HasColumnType("date")
                        .HasColumnName("R_Date");

                    b.Property<byte>("SupplierId")
                        .HasColumnType("tinyint")
                        .HasColumnName("S_Id");

                    b.HasKey("ReceiptId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.ReceiptDetail", b =>
                {
                    b.Property<int>("ReceiptDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RD_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReceiptDetailId"));

                    b.Property<short>("ProductStockId")
                        .HasColumnType("smallint")
                        .HasColumnName("PS_Id");

                    b.Property<int>("ReceiptId")
                        .HasColumnType("int")
                        .HasColumnName("R_Id");

                    b.Property<byte?>("RollCount")
                        .HasColumnType("tinyint")
                        .HasColumnName("Roll_Count");

                    b.Property<float>("UnitRate")
                        .HasColumnType("real")
                        .HasColumnName("Unit_Rate");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("ReceiptDetailId");

                    b.HasIndex("ProductStockId");

                    b.HasIndex("ReceiptId");

                    b.ToTable("ReceiptDetails");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.RollNumber", b =>
                {
                    b.Property<int>("RollNumberId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RN_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RollNumberId"));

                    b.Property<int>("ReceiptDetailId")
                        .HasColumnType("int")
                        .HasColumnName("RD_Id");

                    b.Property<string>("RollNumberValue")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Roll_Number");

                    b.HasKey("RollNumberId");

                    b.HasIndex("ReceiptDetailId");

                    b.ToTable("RollNumbers");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.Size", b =>
                {
                    b.Property<byte>("SizeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("Size_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("SizeId"));

                    b.Property<short>("SizeInMM")
                        .HasColumnType("smallint")
                        .HasColumnName("Size");

                    b.HasKey("SizeId");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.SlittingDetail", b =>
                {
                    b.Property<int>("SlittingDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SD_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SlittingDetailId"));

                    b.Property<float>("Moisture")
                        .HasColumnType("real");

                    b.Property<int>("ProductionSlittingId")
                        .HasColumnType("int")
                        .HasColumnName("PS_Id");

                    b.Property<string>("RollNumber")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Roll_No");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasKey("SlittingDetailId");

                    b.HasIndex("ProductionSlittingId");

                    b.ToTable("SlittingDetails");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.Supplier", b =>
                {
                    b.Property<byte>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("S_Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("SupplierId"));

                    b.Property<double>("Dues")
                        .HasColumnType("float");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Supplier_Name");

                    b.HasKey("SupplierId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.Issue", b =>
                {
                    b.HasOne("CompanyApp.Models.Entity.ProductStock", "ProductStocks")
                        .WithMany("Issues")
                        .HasForeignKey("ProductStockId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CompanyApp.Models.Entity.RollNumber", "RollNumbers")
                        .WithMany("Issues")
                        .HasForeignKey("RollNumberId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("ProductStocks");

                    b.Navigation("RollNumbers");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.ProductDetail", b =>
                {
                    b.HasOne("CompanyApp.Models.Entity.Product", "Products")
                        .WithMany("ProductDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Products");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.ProductStock", b =>
                {
                    b.HasOne("CompanyApp.Models.Entity.ProductDetail", "ProductDetails")
                        .WithMany("ProductStocks")
                        .HasForeignKey("ProductDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CompanyApp.Models.Entity.Size", "Sizes")
                        .WithMany("ProductStocks")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ProductDetails");

                    b.Navigation("Sizes");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.ProductionCalendaring", b =>
                {
                    b.HasOne("CompanyApp.Models.Entity.ProductionCoating", "ProductionCoatings")
                        .WithMany("ProductionCalendarings")
                        .HasForeignKey("ProductionCoatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductionCoatings");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.ProductionCoating", b =>
                {
                    b.HasOne("CompanyApp.Models.Entity.Issue", "Issues")
                        .WithMany("ProductionCoatings")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Issues");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.ProductionSlitting", b =>
                {
                    b.HasOne("CompanyApp.Models.Entity.ProductionCalendaring", "ProductionCalendarings")
                        .WithMany("ProductionSlittings")
                        .HasForeignKey("ProductionCalendaringId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductionCalendarings");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.Receipt", b =>
                {
                    b.HasOne("CompanyApp.Models.Entity.Supplier", "Suppliers")
                        .WithMany("Receipts")
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Suppliers");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.ReceiptDetail", b =>
                {
                    b.HasOne("CompanyApp.Models.Entity.ProductStock", "ProductStocks")
                        .WithMany("ReceiptDetails")
                        .HasForeignKey("ProductStockId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("CompanyApp.Models.Entity.Receipt", "Receipts")
                        .WithMany("ReceiptDetails")
                        .HasForeignKey("ReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductStocks");

                    b.Navigation("Receipts");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.RollNumber", b =>
                {
                    b.HasOne("CompanyApp.Models.Entity.ReceiptDetail", "ReceiptDetails")
                        .WithMany("RollNumbers")
                        .HasForeignKey("ReceiptDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReceiptDetails");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.SlittingDetail", b =>
                {
                    b.HasOne("CompanyApp.Models.Entity.ProductionSlitting", "ProductionSlittings")
                        .WithMany("SlittingDetails")
                        .HasForeignKey("ProductionSlittingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductionSlittings");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.Issue", b =>
                {
                    b.Navigation("ProductionCoatings");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.Product", b =>
                {
                    b.Navigation("ProductDetails");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.ProductDetail", b =>
                {
                    b.Navigation("ProductStocks");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.ProductStock", b =>
                {
                    b.Navigation("Issues");

                    b.Navigation("ReceiptDetails");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.ProductionCalendaring", b =>
                {
                    b.Navigation("ProductionSlittings");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.ProductionCoating", b =>
                {
                    b.Navigation("ProductionCalendarings");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.ProductionSlitting", b =>
                {
                    b.Navigation("SlittingDetails");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.Receipt", b =>
                {
                    b.Navigation("ReceiptDetails");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.ReceiptDetail", b =>
                {
                    b.Navigation("RollNumbers");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.RollNumber", b =>
                {
                    b.Navigation("Issues");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.Size", b =>
                {
                    b.Navigation("ProductStocks");
                });

            modelBuilder.Entity("CompanyApp.Models.Entity.Supplier", b =>
                {
                    b.Navigation("Receipts");
                });
#pragma warning restore 612, 618
        }
    }
}
