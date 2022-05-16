﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SportClubAPI.Models;

#nullable disable

namespace SportClubAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220516090045_SeederAdded")]
    partial class SeederAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CustomerSportGood", b =>
                {
                    b.Property<int>("CustomersCustomerId")
                        .HasColumnType("int");

                    b.Property<int>("SportGoodsSportGoodId")
                        .HasColumnType("int");

                    b.HasKey("CustomersCustomerId", "SportGoodsSportGoodId");

                    b.HasIndex("SportGoodsSportGoodId");

                    b.ToTable("CustomerSportGood");
                });

            modelBuilder.Entity("SportClubWMS.Shared.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("SubscribeStatus")
                        .HasColumnType("int");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            Address = "St.Johnes St, 57",
                            Age = 24,
                            Email = "tom-jackson.@gmail.com",
                            FirstName = "Tom",
                            Gender = 0,
                            RegistrationDate = new DateTime(2022, 5, 16, 12, 0, 45, 644, DateTimeKind.Local).AddTicks(5300),
                            SecondName = "Jackson",
                            SubscribeStatus = 0
                        },
                        new
                        {
                            CustomerId = 2,
                            Address = "TK-center, 5",
                            Age = 60,
                            Email = "meina-gladston.@gmail.com",
                            FirstName = "Meina",
                            Gender = 1,
                            RegistrationDate = new DateTime(2022, 5, 16, 12, 0, 45, 644, DateTimeKind.Local).AddTicks(5360),
                            SecondName = "Gladston",
                            SubscribeStatus = 1
                        },
                        new
                        {
                            CustomerId = 3,
                            Address = "Jackson St., 24",
                            Age = 50,
                            Email = "John-batista.@gmail.com",
                            FirstName = "John",
                            Gender = 0,
                            RegistrationDate = new DateTime(2022, 5, 16, 12, 0, 45, 644, DateTimeKind.Local).AddTicks(5370),
                            SecondName = "Batista",
                            SubscribeStatus = 1
                        },
                        new
                        {
                            CustomerId = 4,
                            Address = "Britain St., 77",
                            Age = 52,
                            Email = "Boris-Johnson.@gmail.com",
                            FirstName = "Boris",
                            Gender = 0,
                            RegistrationDate = new DateTime(2022, 5, 16, 12, 0, 45, 644, DateTimeKind.Local).AddTicks(5379),
                            SecondName = "Johnson",
                            SubscribeStatus = 0
                        },
                        new
                        {
                            CustomerId = 5,
                            Address = "Ladozhskaya St., 15",
                            Age = 22,
                            Email = "Vlad-Kutepov.@gmail.com",
                            FirstName = "Vladislav",
                            Gender = 0,
                            RegistrationDate = new DateTime(2022, 5, 16, 12, 0, 45, 644, DateTimeKind.Local).AddTicks(5389),
                            SecondName = "Kutepov",
                            SubscribeStatus = 0
                        },
                        new
                        {
                            CustomerId = 6,
                            Address = "Moskovskaya St., 15",
                            Age = 30,
                            Email = "Vlad-Kutepov.@gmail.com",
                            FirstName = "Evgeniy",
                            Gender = 0,
                            RegistrationDate = new DateTime(2022, 5, 16, 12, 0, 45, 644, DateTimeKind.Local).AddTicks(5401),
                            SecondName = "Bazhenov",
                            SubscribeStatus = 1
                        },
                        new
                        {
                            CustomerId = 7,
                            Address = "Komsomolskaya St., 15",
                            Age = 40,
                            Email = "elena-kapustkina.@gmail.com",
                            FirstName = "Elena",
                            Gender = 1,
                            RegistrationDate = new DateTime(2022, 5, 16, 12, 0, 45, 644, DateTimeKind.Local).AddTicks(5410),
                            SecondName = "Kapustovna",
                            SubscribeStatus = 1
                        });
                });

            modelBuilder.Entity("SportClubWMS.Shared.CustomerSportGood", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint");

                    b.Property<int>("SportGoodId")
                        .HasColumnType("int");

                    b.Property<string>("SportGoodName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerSportGoods");
                });

            modelBuilder.Entity("SportClubWMS.Shared.SportGood", b =>
                {
                    b.Property<int>("SportGoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SportGoodId"), 1L, 1);

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<long>("Quantity")
                        .HasColumnType("bigint");

                    b.HasKey("SportGoodId");

                    b.ToTable("SportGoods");

                    b.HasData(
                        new
                        {
                            SportGoodId = 1,
                            Category = 0,
                            Description = "A ball that is used in a football game",
                            Name = "Football ball",
                            Quantity = 20L
                        },
                        new
                        {
                            SportGoodId = 2,
                            Category = 1,
                            Description = "A ball that is used in a basketball game",
                            Name = "Basketball ball",
                            Quantity = 15L
                        },
                        new
                        {
                            SportGoodId = 3,
                            Category = 5,
                            Description = "A good that is used for a faster swimming",
                            Name = "Slippers",
                            Quantity = 25L
                        },
                        new
                        {
                            SportGoodId = 4,
                            Category = 4,
                            Description = "It is used for punching a tennis ball",
                            Name = "Tennis crocket",
                            Quantity = 30L
                        },
                        new
                        {
                            SportGoodId = 5,
                            Category = 4,
                            Description = "A ball that is used in a tennis game",
                            Name = "Tennis ball",
                            Quantity = 15L
                        });
                });

            modelBuilder.Entity("CustomerSportGood", b =>
                {
                    b.HasOne("SportClubWMS.Shared.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomersCustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SportClubWMS.Shared.SportGood", null)
                        .WithMany()
                        .HasForeignKey("SportGoodsSportGoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportClubWMS.Shared.CustomerSportGood", b =>
                {
                    b.HasOne("SportClubWMS.Shared.Customer", null)
                        .WithMany("CustomerSportGoods")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SportClubWMS.Shared.Customer", b =>
                {
                    b.Navigation("CustomerSportGoods");
                });
#pragma warning restore 612, 618
        }
    }
}
