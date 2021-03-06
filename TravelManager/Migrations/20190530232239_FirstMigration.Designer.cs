﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TravelManager.Models;

namespace TravelManager.Migrations
{
    [DbContext(typeof(TravelManagerContext))]
    [Migration("20190530232239_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TravelManager.Models.Currency", b =>
                {
                    b.Property<long>("CurrencyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.HasKey("CurrencyId");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("TravelManager.Models.Document", b =>
                {
                    b.Property<long>("DocumentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("DownloadLink");

                    b.Property<string>("Name");

                    b.HasKey("DocumentId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("TravelManager.Models.DocumentAsignee", b =>
                {
                    b.Property<long>("DocumentAsigneeId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("DocumentId");

                    b.Property<long>("EmployeeId");

                    b.Property<long>("EventId");

                    b.HasKey("DocumentAsigneeId");

                    b.HasIndex("DocumentId")
                        .IsUnique();

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.HasIndex("EventId");

                    b.ToTable("DocumentAsignees");
                });

            modelBuilder.Entity("TravelManager.Models.Employee", b =>
                {
                    b.Property<long>("EmployeeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("MiddleName");

                    b.Property<string>("Name");

                    b.Property<string>("Position");

                    b.Property<string>("Surname");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("TravelManager.Models.Event", b =>
                {
                    b.Property<long>("EventId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Cost");

                    b.Property<long>("CurrencyId");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("Location");

                    b.Property<string>("Partners");

                    b.Property<long>("PlaceId");

                    b.Property<bool>("Planned");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("EventId");

                    b.HasIndex("CurrencyId");

                    b.HasIndex("PlaceId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("TravelManager.Models.ExchangeRate", b =>
                {
                    b.Property<long>("ExchangeRateId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("FirstCurrencyId");

                    b.Property<double>("Rate");

                    b.Property<long>("SecondCurrencyId");

                    b.HasKey("ExchangeRateId");

                    b.HasIndex("FirstCurrencyId");

                    b.HasIndex("SecondCurrencyId");

                    b.ToTable("ExchangeRates");
                });

            modelBuilder.Entity("TravelManager.Models.Place", b =>
                {
                    b.Property<long>("PlaceId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<byte>("Priority");

                    b.Property<string>("Type");

                    b.Property<string>("Website");

                    b.HasKey("PlaceId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("TravelManager.Models.PlaceDocument", b =>
                {
                    b.Property<long>("DocumentId");

                    b.Property<long>("PlaceId");

                    b.HasKey("DocumentId", "PlaceId");

                    b.HasIndex("PlaceId");

                    b.ToTable("PlaceDocuments");
                });

            modelBuilder.Entity("TravelManager.Models.PlaceRole", b =>
                {
                    b.Property<long>("PlaceId");

                    b.Property<long>("RoleId");

                    b.HasKey("PlaceId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("PlaceRoles");
                });

            modelBuilder.Entity("TravelManager.Models.Role", b =>
                {
                    b.Property<long>("RoleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("TravelManager.Models.RoleAsignee", b =>
                {
                    b.Property<long>("RoleAsigneeId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("EmployeeId");

                    b.Property<long>("EventId");

                    b.Property<long>("RoleId");

                    b.HasKey("RoleAsigneeId");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.HasIndex("EventId");

                    b.HasIndex("RoleId")
                        .IsUnique();

                    b.ToTable("RoleAsignees");
                });

            modelBuilder.Entity("TravelManager.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CurrencyId");

                    b.Property<string>("Email");

                    b.Property<string>("IdentityId");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("UserId");

                    b.HasIndex("CurrencyId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TravelManager.Models.UserIdentity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TravelManager.Models.UserIdentity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TravelManager.Models.UserIdentity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TravelManager.Models.UserIdentity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("TravelManager.Models.UserIdentity")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TravelManager.Models.DocumentAsignee", b =>
                {
                    b.HasOne("TravelManager.Models.Document", "Document")
                        .WithOne("DocumentAsignee")
                        .HasForeignKey("TravelManager.Models.DocumentAsignee", "DocumentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TravelManager.Models.Employee", "Employee")
                        .WithOne("DocumentAsignee")
                        .HasForeignKey("TravelManager.Models.DocumentAsignee", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TravelManager.Models.Event", "Event")
                        .WithMany("DocumentAsignees")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TravelManager.Models.Event", b =>
                {
                    b.HasOne("TravelManager.Models.Currency", "Currency")
                        .WithMany("Events")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TravelManager.Models.Place", "Place")
                        .WithMany("Events")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TravelManager.Models.ExchangeRate", b =>
                {
                    b.HasOne("TravelManager.Models.Currency", "FirstCurrency")
                        .WithMany("FirstExchangeRates")
                        .HasForeignKey("FirstCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TravelManager.Models.Currency", "SecondCurrency")
                        .WithMany("SecondExchangeRates")
                        .HasForeignKey("SecondCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TravelManager.Models.PlaceDocument", b =>
                {
                    b.HasOne("TravelManager.Models.Document", "Document")
                        .WithMany("PlaceDocuments")
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TravelManager.Models.Place", "Place")
                        .WithMany("PlaceDocuments")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TravelManager.Models.PlaceRole", b =>
                {
                    b.HasOne("TravelManager.Models.Place", "Place")
                        .WithMany("PlaceRoles")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TravelManager.Models.Role", "Role")
                        .WithMany("PlaceRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TravelManager.Models.RoleAsignee", b =>
                {
                    b.HasOne("TravelManager.Models.Employee", "Employee")
                        .WithOne("RoleAsignee")
                        .HasForeignKey("TravelManager.Models.RoleAsignee", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TravelManager.Models.Event", "Event")
                        .WithMany("RoleAsignees")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TravelManager.Models.Role", "Role")
                        .WithOne("RoleAsignee")
                        .HasForeignKey("TravelManager.Models.RoleAsignee", "RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TravelManager.Models.User", b =>
                {
                    b.HasOne("TravelManager.Models.Currency", "Currency")
                        .WithMany("Users")
                        .HasForeignKey("CurrencyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
