﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicketSelling.Context;

#nullable disable

namespace TicketSelling.Context.Migrations
{
    [DbContext(typeof(TicketSellingContext))]
    [Migration("20231022121254_Initial Migratioin")]
    partial class InitialMigratioin
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TicketSelling.Context.Contracts.Models.Cinema", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Cinemas");
                });

            modelBuilder.Entity("TicketSelling.Context.Contracts.Models.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("Age")
                        .HasMaxLength(2)
                        .HasColumnType("smallint");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("TicketSelling.Context.Contracts.Models.Film", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<short>("Limitation")
                        .HasMaxLength(2)
                        .HasColumnType("smallint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Films");
                });

            modelBuilder.Entity("TicketSelling.Context.Contracts.Models.Hall", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("Number")
                        .HasMaxLength(2)
                        .HasColumnType("smallint");

                    b.Property<short>("NumberOfSeats")
                        .HasMaxLength(3)
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("Halls");
                });

            modelBuilder.Entity("TicketSelling.Context.Contracts.Models.Staff", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("Age")
                        .HasMaxLength(2)
                        .HasColumnType("smallint");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Post")
                        .HasMaxLength(1)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("TicketSelling.Context.Contracts.Models.Ticket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CinemaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("FilmId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("HallId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<short>("Place")
                        .HasMaxLength(2)
                        .HasColumnType("smallint");

                    b.Property<decimal>("Price")
                        .HasMaxLength(5)
                        .HasColumnType("decimal(18,2)");

                    b.Property<short>("Row")
                        .HasMaxLength(2)
                        .HasColumnType("smallint");

                    b.Property<Guid>("StaffId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CinemaId");

                    b.HasIndex("ClientId");

                    b.HasIndex("FilmId");

                    b.HasIndex("HallId");

                    b.HasIndex("StaffId");

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("TicketSelling.Context.Contracts.Models.Ticket", b =>
                {
                    b.HasOne("TicketSelling.Context.Contracts.Models.Cinema", "Cinema")
                        .WithMany("Tickets")
                        .HasForeignKey("CinemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketSelling.Context.Contracts.Models.Client", "Client")
                        .WithMany("Tickets")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketSelling.Context.Contracts.Models.Film", "Film")
                        .WithMany("Tickets")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketSelling.Context.Contracts.Models.Hall", "Hall")
                        .WithMany("Tickets")
                        .HasForeignKey("HallId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketSelling.Context.Contracts.Models.Staff", "Staff")
                        .WithMany("Tickets")
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cinema");

                    b.Navigation("Client");

                    b.Navigation("Film");

                    b.Navigation("Hall");

                    b.Navigation("Staff");
                });

            modelBuilder.Entity("TicketSelling.Context.Contracts.Models.Cinema", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("TicketSelling.Context.Contracts.Models.Client", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("TicketSelling.Context.Contracts.Models.Film", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("TicketSelling.Context.Contracts.Models.Hall", b =>
                {
                    b.Navigation("Tickets");
                });

            modelBuilder.Entity("TicketSelling.Context.Contracts.Models.Staff", b =>
                {
                    b.Navigation("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
