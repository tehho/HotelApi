﻿// <auto-generated />
using Hotel.Infrastructure.DbManager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Hotel.Infrastructure.Migrations
{
    [DbContext(typeof(HotelContext))]
    [Migration("20180515091407_HotelClass")]
    partial class HotelClass
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hotel.Domain.Hotel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("HotelRegionId")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("RoomsAvailable");

                    b.HasKey("Id");

                    b.HasIndex("HotelRegionId");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("Hotel.Domain.HotelRegion", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("HotelRegions");
                });

            modelBuilder.Entity("Hotel.Domain.Hotel", b =>
                {
                    b.HasOne("Hotel.Domain.HotelRegion", "Region")
                        .WithMany("Hotels")
                        .HasForeignKey("HotelRegionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
