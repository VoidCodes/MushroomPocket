﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MushroomPocket.Context;

#nullable disable

namespace MushroomPocket.Migrations
{
    [DbContext(typeof(Dbcontext))]
    [Migration("20240519173431_AddItemAndInventoryTables")]
    partial class AddItemAndInventoryTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("MushroomPocket.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CharacterName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Exp")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExpToTransform")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Hp")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Skill")
                        .HasColumnType("TEXT");

                    b.Property<string>("TransformTo")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Character");
                });

            modelBuilder.Entity("MushroomPocket.Models.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CharacterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ItemsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("ItemsId");

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("MushroomPocket.Models.Items", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Effect")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("MushroomPocket.Models.Inventory", b =>
                {
                    b.HasOne("MushroomPocket.Models.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MushroomPocket.Models.Items", "Items")
                        .WithMany()
                        .HasForeignKey("ItemsId");

                    b.Navigation("Character");

                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
