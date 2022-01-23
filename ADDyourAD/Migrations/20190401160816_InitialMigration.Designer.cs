﻿// <auto-generated />
using System;
using ADDyourAD.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ADDyourAD.Migrations
{
    [DbContext(typeof(AdvertisementDBContext))]
    [Migration("20190401160816_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ADDyourAD.Models.Admin", b =>
                {
                    b.Property<int>("IdAdmin")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdAdmin");

                    b.ToTable("Admin");
                });

            modelBuilder.Entity("ADDyourAD.Models.Advertisement", b =>
                {
                    b.Property<int>("IdAdvertisement")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID_Advertisement")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddDate")
                        .HasColumnName("Add_Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Details");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnName("Expiration_Date")
                        .HasColumnType("datetime");

                    b.Property<int>("IdCategory")
                        .HasColumnName("ID_Category");

                    b.Property<int>("IdUser")
                        .HasColumnName("ID_User");

                    b.Property<byte[]>("Image")
                        .HasColumnType("image");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdAdvertisement");

                    b.HasIndex("IdCategory");

                    b.HasIndex("IdUser");

                    b.ToTable("Advertisement");
                });

            modelBuilder.Entity("ADDyourAD.Models.Category", b =>
                {
                    b.Property<int>("IdCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID_Category")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnName("Category_Name")
                        .HasMaxLength(50);

                    b.HasKey("IdCategory");

                    b.HasIndex("CategoryName")
                        .IsUnique()
                        .HasName("IX_Category");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ADDyourAD.Models.Comment", b =>
                {
                    b.Property<int>("IdComment")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID_Comment")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime");

                    b.Property<int>("IdAdvertisement")
                        .HasColumnName("ID_Advertisement");

                    b.Property<int>("IdUser")
                        .HasColumnName("ID_User");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("IdComment");

                    b.HasIndex("IdAdvertisement");

                    b.HasIndex("IdUser");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("ADDyourAD.Models.User", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID_User")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnName("First_Name")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .HasColumnName("Last_Name")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PhoneNumber")
                        .HasColumnName("Phone_Number")
                        .HasMaxLength(50);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("IdUser");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ADDyourAD.Models.Advertisement", b =>
                {
                    b.HasOne("ADDyourAD.Models.Category", "IdCategoryNavigation")
                        .WithMany("Advertisement")
                        .HasForeignKey("IdCategory")
                        .HasConstraintName("FK_Advertisement_Category");

                    b.HasOne("ADDyourAD.Models.User", "IdUserNavigation")
                        .WithMany("Advertisement")
                        .HasForeignKey("IdUser")
                        .HasConstraintName("FK_Advertisement_User");
                });

            modelBuilder.Entity("ADDyourAD.Models.Comment", b =>
                {
                    b.HasOne("ADDyourAD.Models.Advertisement", "IdAdvertisementNavigation")
                        .WithMany("Comment")
                        .HasForeignKey("IdAdvertisement")
                        .HasConstraintName("FK_Comment_Advertisement");

                    b.HasOne("ADDyourAD.Models.User", "IdUserNavigation")
                        .WithMany("Comment")
                        .HasForeignKey("IdUser")
                        .HasConstraintName("FK_Comment_User");
                });
#pragma warning restore 612, 618
        }
    }
}