﻿// <auto-generated />
using System;
using Guacamole.Minimal.Contexts;
using Guacamole.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Guacamole.Minimal.Migrations
{
    [DbContext(typeof(BluntContext))]
    [Migration("20221117135203_DateOnlyToDateTime")]
    partial class DateOnlyToDateTime
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("Guacamole.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2022, 11, 17, 10, 52, 3, 464, DateTimeKind.Local).AddTicks(9104),
                            Name = "Default"
                        });
                });

            modelBuilder.Entity("Guacamole.Models.Idea", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Archived")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Ideas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Archived = false,
                            CategoryId = 1,
                            Content = "My first idea!",
                            Created = new DateTime(2022, 11, 17, 10, 52, 3, 464, DateTimeKind.Local).AddTicks(9446),
                            Modified = new DateTime(2022, 11, 17, 10, 52, 3, 464, DateTimeKind.Local).AddTicks(9452)
                        });
                });

            modelBuilder.Entity("Guacamole.Models.Idea", b =>
                {
                    b.HasOne("Guacamole.Models.Category", "Category")
                        .WithMany("Ideas")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Guacamole.Models.Category", b =>
                {
                    b.Navigation("Ideas");
                });
#pragma warning restore 612, 618
        }
    }
}