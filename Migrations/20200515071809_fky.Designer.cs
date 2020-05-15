﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PVE.Data;

namespace PVE.Migrations
{
    [DbContext(typeof(PVEContext))]
    [Migration("20200515071809_fky")]
    partial class fky
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PVE.Models.PveData", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Agreement")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BOB")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactCATAC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactCustomer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactMarket")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContractType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("FeeJ1")
                        .HasColumnType("float");

                    b.Property<double?>("FeeJ2")
                        .HasColumnType("float");

                    b.Property<double?>("FeeJ3")
                        .HasColumnType("float");

                    b.Property<string>("FeeStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OBD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Period")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Producer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProgressJ1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProgressJ2D")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProgressJ2H")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProgressJ2S")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProgressJ2W")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProgressJ2Z")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProgressJ3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("ProjectBid")
                        .HasColumnType("float");

                    b.Property<string>("ProjectStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ReportDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SerialNum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaskForm")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TestContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VIN")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("VehicleNum")
                        .HasColumnType("int");

                    b.Property<string>("VehicleType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("VIN")
                        .IsUnique();

                    b.ToTable("PveData");
                });

            modelBuilder.Entity("PVE.Models.Signal", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Func1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Func2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OBD")
                        .HasColumnType("bit");

                    b.Property<string>("PinName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PinNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PveDataID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PveDataID");

                    b.ToTable("Signal");
                });

            modelBuilder.Entity("PVE.Models.Signal", b =>
                {
                    b.HasOne("PVE.Models.PveData", "PveData")
                        .WithMany("Signals")
                        .HasForeignKey("PveDataID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
