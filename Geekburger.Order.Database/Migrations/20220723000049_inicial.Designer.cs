﻿// <auto-generated />
using Geekburger.Order.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Geekburger.Order.Database.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    [Migration("20220723000049_inicial")]
    partial class inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.7");

            modelBuilder.Entity("Geekburger.Order.Domain.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<double>("Total")
                        .HasColumnType("REAL");

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Geekburger.Order.Domain.Entities.Product", b =>
                {
                    b.Property<string>("ProductId")
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrdersProducts");
                });

            modelBuilder.Entity("Geekburger.Order.Domain.Entities.Production", b =>
                {
                    b.Property<int>("ProductionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductionId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrdersProduction");
                });

            modelBuilder.Entity("Geekburger.Order.Domain.Entities.Product", b =>
                {
                    b.HasOne("Geekburger.Order.Domain.Entities.Order", null)
                        .WithMany("Products")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Geekburger.Order.Domain.Entities.Production", b =>
                {
                    b.HasOne("Geekburger.Order.Domain.Entities.Order", null)
                        .WithMany("Productions")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Geekburger.Order.Domain.Entities.Order", b =>
                {
                    b.Navigation("Productions");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
