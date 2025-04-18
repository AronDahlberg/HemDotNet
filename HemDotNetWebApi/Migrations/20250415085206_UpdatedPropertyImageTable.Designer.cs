﻿// <auto-generated />
using System;
using HemDotNetWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HemDotNetWebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250415085206_UpdatedPropertyImageTable")]
    partial class UpdatedPropertyImageTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HemDotNetWebApi.Models.MarketProperty", b =>
                {
                    b.Property<int>("MarketPropertyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MarketPropertyId"));

                    b.Property<int>("AmountOfRooms")
                        .HasColumnType("int");

                    b.Property<double>("AncillaryArea")
                        .HasColumnType("float");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<int>("ContructionYear")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<double>("LivingArea")
                        .HasColumnType("float");

                    b.Property<double>("LotArea")
                        .HasColumnType("float");

                    b.Property<decimal?>("MonthlyFee")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("MunicipalityId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("PropertyAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RealEstateAgentId")
                        .HasColumnType("int");

                    b.Property<decimal?>("YearlyMaintenanceCost")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("MarketPropertyId");

                    b.HasIndex("MunicipalityId");

                    b.HasIndex("RealEstateAgentId");

                    b.ToTable("MarketProperties");
                });

            modelBuilder.Entity("HemDotNetWebApi.Models.Municipality", b =>
                {
                    b.Property<int>("MunicipalityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MunicipalityId"));

                    b.Property<string>("MunicipalityName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("MunicipalityId");

                    b.ToTable("Municipalities");
                });

            modelBuilder.Entity("HemDotNetWebApi.Models.PropertyImage", b =>
                {
                    b.Property<int>("PropertyImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PropertyImageId"));

                    b.Property<int>("MarketPropertyId")
                        .HasColumnType("int");

                    b.Property<string>("PropertyImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PropertyImageId");

                    b.HasIndex("MarketPropertyId");

                    b.ToTable("PropertyImages");
                });

            modelBuilder.Entity("HemDotNetWebApi.Models.RealEstateAgency", b =>
                {
                    b.Property<int>("RealEstateAgencyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RealEstateAgencyId"));

                    b.Property<string>("RealEstateAgencyLogoUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RealEstateAgencyName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("RealEstateAgencyPresentation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RealEstateAgencyId");

                    b.ToTable("RealEstateAgencies");
                });

            modelBuilder.Entity("HemDotNetWebApi.Models.RealEstateAgent", b =>
                {
                    b.Property<int>("RealEstateAgentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RealEstateAgentId"));

                    b.Property<int>("RealEstateAgency")
                        .HasColumnType("int");

                    b.Property<string>("RealEstateAgentEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RealEstateAgentFirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("RealEstateAgentImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RealEstateAgentLastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("RealEstateAgentPhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RealEstateAgentId");

                    b.HasIndex("RealEstateAgency");

                    b.ToTable("RealEstateAgents");
                });

            modelBuilder.Entity("HemDotNetWebApi.Models.MarketProperty", b =>
                {
                    b.HasOne("HemDotNetWebApi.Models.Municipality", "Municipality")
                        .WithMany()
                        .HasForeignKey("MunicipalityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HemDotNetWebApi.Models.RealEstateAgent", "RealEstateAgent")
                        .WithMany()
                        .HasForeignKey("RealEstateAgentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Municipality");

                    b.Navigation("RealEstateAgent");
                });

            modelBuilder.Entity("HemDotNetWebApi.Models.PropertyImage", b =>
                {
                    b.HasOne("HemDotNetWebApi.Models.MarketProperty", "MarketProperty")
                        .WithMany("Images")
                        .HasForeignKey("MarketPropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MarketProperty");
                });

            modelBuilder.Entity("HemDotNetWebApi.Models.RealEstateAgent", b =>
                {
                    b.HasOne("HemDotNetWebApi.Models.RealEstateAgency", "RealEstateAgentAgency")
                        .WithMany()
                        .HasForeignKey("RealEstateAgency")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RealEstateAgentAgency");
                });

            modelBuilder.Entity("HemDotNetWebApi.Models.MarketProperty", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
