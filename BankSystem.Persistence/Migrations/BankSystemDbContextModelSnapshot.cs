﻿// <auto-generated />
using System;
using BankSystem.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankSystem.Persistence.Migrations
{
    [DbContext(typeof(BankSystemDbContext))]
    partial class BankSystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("bankdb")
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BankSystem.Core.Domain.Cards.Models.Card", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CVV2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Credit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateOnly>("ExpirationDate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("IssueDate")
                        .HasColumnType("date");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentSystem")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cards", "bankdb");
                });

            modelBuilder.Entity("BankSystem.Core.Domain.Cards.Models.ClientsCards", b =>
                {
                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CardId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ClientId", "CardId");

                    b.HasIndex("CardId");

                    b.ToTable("ClientsCards", "bankdb");
                });

            modelBuilder.Entity("BankSystem.Core.Domain.Clients.Models.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Clients", "bankdb");
                });

            modelBuilder.Entity("BankSystem.Core.Domain.Cards.Models.ClientsCards", b =>
                {
                    b.HasOne("BankSystem.Core.Domain.Cards.Models.Card", "Card")
                        .WithMany("ClientsCards")
                        .HasForeignKey("CardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BankSystem.Core.Domain.Clients.Models.Client", "Client")
                        .WithMany("ClientsCards")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Card");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("BankSystem.Core.Domain.Cards.Models.Card", b =>
                {
                    b.Navigation("ClientsCards");
                });

            modelBuilder.Entity("BankSystem.Core.Domain.Clients.Models.Client", b =>
                {
                    b.Navigation("ClientsCards");
                });
#pragma warning restore 612, 618
        }
    }
}