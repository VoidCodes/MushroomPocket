﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MushroomPocket.Context;

#nullable disable

namespace MushroomPocket.Migrations
{
    [DbContext(typeof(Dbcontext))]
    partial class DbcontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("EffectType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EffectValue")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemName")
                        .HasColumnType("TEXT");

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
                        .WithMany("Inventory")
                        .HasForeignKey("ItemsId");

                    b.Navigation("Character");

                    b.Navigation("Items");
                });

            modelBuilder.Entity("MushroomPocket.Models.Items", b =>
                {
                    b.Navigation("Inventory");
                });
#pragma warning restore 612, 618
        }
    }
}
