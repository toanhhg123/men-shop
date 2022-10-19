﻿// <auto-generated />
using Male.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Male.Migrations
{
    [DbContext(typeof(MyDBContext))]
    partial class MyDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Male.Models.Account", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("newid()");

                    b.Property<string>("Roleid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("hashPassword")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("id");

                    b.HasIndex("Roleid");

                    b.HasIndex("email")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("Male.Models.Blog", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("newid()");

                    b.Property<string>("Auth")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Desc1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Desc2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Desc3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Desc4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Img")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("Male.Models.Brand", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("newid()");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Male.Models.Cart", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("newid()");

                    b.Property<string>("Accountid")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("productid")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("id");

                    b.HasIndex("Accountid");

                    b.HasIndex("productid");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("Male.Models.Category", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("newid()");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Male.Models.Order", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("newid()");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("accountid")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("isConfirm")
                        .HasColumnType("bit");

                    b.Property<string>("productid")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("id");

                    b.HasIndex("accountid");

                    b.HasIndex("productid");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Male.Models.Product", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("newid()");

                    b.Property<string>("Brandid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CountStock")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("categoryid")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("img1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("img2")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("img3")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("img4")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("rating")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Brandid");

                    b.HasIndex("categoryid");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Male.Models.Role", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("newid()");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Male.Models.Account", b =>
                {
                    b.HasOne("Male.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("Roleid");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Male.Models.Cart", b =>
                {
                    b.HasOne("Male.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("Accountid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Male.Models.Product", "product")
                        .WithMany()
                        .HasForeignKey("productid");

                    b.Navigation("Account");

                    b.Navigation("product");
                });

            modelBuilder.Entity("Male.Models.Order", b =>
                {
                    b.HasOne("Male.Models.Account", "account")
                        .WithMany()
                        .HasForeignKey("accountid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Male.Models.Product", "product")
                        .WithMany()
                        .HasForeignKey("productid");

                    b.Navigation("account");

                    b.Navigation("product");
                });

            modelBuilder.Entity("Male.Models.Product", b =>
                {
                    b.HasOne("Male.Models.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("Brandid");

                    b.HasOne("Male.Models.Category", "category")
                        .WithMany()
                        .HasForeignKey("categoryid");

                    b.Navigation("Brand");

                    b.Navigation("category");
                });
#pragma warning restore 612, 618
        }
    }
}
