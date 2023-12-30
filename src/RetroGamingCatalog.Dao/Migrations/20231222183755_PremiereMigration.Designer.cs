﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RetroGamingCatalog.Dao;

#nullable disable

namespace RetroGamingCatalog.Dao.Migrations
{
    [DbContext(typeof(CatalogDb))]
    [Migration("20231222183755_PremiereMigration")]
    partial class PremiereMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RetroGamingCatalog.Dao.Console", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ManufacturerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Consoles");
                });

            modelBuilder.Entity("RetroGamingCatalog.Dao.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConsoleId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ConsoleId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("RetroGamingCatalog.Dao.Manufacturer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("RetroGamingCatalog.Dao.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastConnectionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Locked")
                        .HasColumnType("boolean");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserType")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RetroGamingCatalog.Dao.Console", b =>
                {
                    b.HasOne("RetroGamingCatalog.Dao.Manufacturer", "Manufacturer")
                        .WithMany("Consoles")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("RetroGamingCatalog.Dao.Game", b =>
                {
                    b.HasOne("RetroGamingCatalog.Dao.Console", "Console")
                        .WithMany("Games")
                        .HasForeignKey("ConsoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Console");
                });

            modelBuilder.Entity("RetroGamingCatalog.Dao.Console", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("RetroGamingCatalog.Dao.Manufacturer", b =>
                {
                    b.Navigation("Consoles");
                });
#pragma warning restore 612, 618
        }
    }
}
