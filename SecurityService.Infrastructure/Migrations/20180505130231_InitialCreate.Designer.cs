﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SecurityService.Core.Models;
using SecurityService.Infrastructure.Database;
using System;

namespace SecurityService.Infrastructure.Migrations
{
    [DbContext(typeof(SecurityDbContext))]
    [Migration("20180505130231_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SecurityService.Core.Models.Container", b =>
                {
                    b.Property<string>("Number")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProductName");

                    b.HasKey("Number");

                    b.HasIndex("ProductName");

                    b.ToTable("Container");
                });

            modelBuilder.Entity("SecurityService.Core.Models.Product", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Type");

                    b.HasKey("Name");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("SecurityService.Core.Models.Truck", b =>
                {
                    b.Property<string>("LicensePlate")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContainerNumber");

                    b.Property<int>("SecurityStatus");

                    b.Property<int>("Status");

                    b.HasKey("LicensePlate");

                    b.HasIndex("ContainerNumber");

                    b.ToTable("Trucks");
                });

            modelBuilder.Entity("SecurityService.Core.Models.Container", b =>
                {
                    b.HasOne("SecurityService.Core.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductName");
                });

            modelBuilder.Entity("SecurityService.Core.Models.Truck", b =>
                {
                    b.HasOne("SecurityService.Core.Models.Container", "Container")
                        .WithMany()
                        .HasForeignKey("ContainerNumber");
                });
#pragma warning restore 612, 618
        }
    }
}