﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence.Contexts;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(CapellaDbContext))]
    [Migration("20230106185708_mig_variant")]
    partial class mig_variant
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductsId")
                        .HasColumnType("integer");

                    b.HasKey("CategoriesId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("ProductsCategories", (string)null);
                });

            modelBuilder.Entity("ClassificationAttributeValueOptions", b =>
                {
                    b.Property<int>("ClassificationAttributeValuesId")
                        .HasColumnType("integer");

                    b.Property<int>("OptionsId")
                        .HasColumnType("integer");

                    b.HasKey("ClassificationAttributeValuesId", "OptionsId");

                    b.HasIndex("OptionsId");

                    b.ToTable("ClassificationAttributeValuesOptions", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Neighbourhood")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Level")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.Entities.Classification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("DataType")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Classifications");
                });

            modelBuilder.Entity("Domain.Entities.ClassificationAttributeValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassificationId")
                        .HasColumnType("integer");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.Property<int?>("VariantItemId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClassificationId");

                    b.HasIndex("ProductId");

                    b.HasIndex("VariantItemId");

                    b.ToTable("ClassificationAttributeValues");
                });

            modelBuilder.Entity("Domain.Entities.Gallery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("VariantItemId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("VariantItemId");

                    b.ToTable("Gallery");
                });

            modelBuilder.Entity("Domain.Entities.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AbsolutePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("EncodedFilename")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("GalleryId")
                        .HasColumnType("integer");

                    b.Property<string>("Mime")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RealFilename")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("RootPath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Secure")
                        .HasColumnType("boolean");

                    b.Property<string>("ServePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GalleryId");

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("Domain.Entities.MediaFormat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("MediaFormats");
                });

            modelBuilder.Entity("Domain.Entities.Options", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassificationId")
                        .HasColumnType("integer");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClassificationId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("Domain.Entities.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Enddate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Startdate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Domain.Entities.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Entities.Variant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChooseType")
                        .HasColumnType("integer");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Variants");
                });

            modelBuilder.Entity("Domain.Entities.VariantItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VariantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("VariantId");

                    b.ToTable("VariantItems");
                });

            modelBuilder.Entity("Domain.Entities.VariantValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("VariantId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("VariantId");

                    b.ToTable("VariantValues");
                });

            modelBuilder.Entity("GalleryProduct", b =>
                {
                    b.Property<int>("GalleriesId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductsId")
                        .HasColumnType("integer");

                    b.HasKey("GalleriesId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("GalleryProduct");
                });

            modelBuilder.Entity("MediaProduct", b =>
                {
                    b.Property<int>("MediasId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductsId")
                        .HasColumnType("integer");

                    b.HasKey("MediasId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("ProductsMedias", (string)null);
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.Property<int>("PermissionsId")
                        .HasColumnType("integer");

                    b.Property<int>("RolesId")
                        .HasColumnType("integer");

                    b.HasKey("PermissionsId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("RolesPermissions", (string)null);
                });

            modelBuilder.Entity("ProductVariant", b =>
                {
                    b.Property<int>("ProductsId")
                        .HasColumnType("integer");

                    b.Property<int>("VariantsId")
                        .HasColumnType("integer");

                    b.HasKey("ProductsId", "VariantsId");

                    b.HasIndex("VariantsId");

                    b.ToTable("ProductsVariants", (string)null);
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("integer");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("CategoryProduct", b =>
                {
                    b.HasOne("Domain.Entities.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClassificationAttributeValueOptions", b =>
                {
                    b.HasOne("Domain.Entities.ClassificationAttributeValue", null)
                        .WithMany()
                        .HasForeignKey("ClassificationAttributeValuesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Options", null)
                        .WithMany()
                        .HasForeignKey("OptionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Address", b =>
                {
                    b.HasOne("Domain.Entities.User", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.HasOne("Domain.Entities.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("Domain.Entities.Classification", b =>
                {
                    b.HasOne("Domain.Entities.Category", null)
                        .WithMany("Classifications")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("Domain.Entities.ClassificationAttributeValue", b =>
                {
                    b.HasOne("Domain.Entities.Classification", "Classification")
                        .WithMany()
                        .HasForeignKey("ClassificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Product", "Product")
                        .WithMany("ClassificationAttributeValues")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.VariantItem", null)
                        .WithMany("ClassificationAttributeValues")
                        .HasForeignKey("VariantItemId");

                    b.Navigation("Classification");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Entities.Gallery", b =>
                {
                    b.HasOne("Domain.Entities.VariantItem", null)
                        .WithMany("Galleries")
                        .HasForeignKey("VariantItemId");
                });

            modelBuilder.Entity("Domain.Entities.Media", b =>
                {
                    b.HasOne("Domain.Entities.Gallery", null)
                        .WithMany("Medias")
                        .HasForeignKey("GalleryId");
                });

            modelBuilder.Entity("Domain.Entities.Options", b =>
                {
                    b.HasOne("Domain.Entities.Classification", "Classification")
                        .WithMany("Options")
                        .HasForeignKey("ClassificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classification");
                });

            modelBuilder.Entity("Domain.Entities.VariantItem", b =>
                {
                    b.HasOne("Domain.Entities.Variant", "Variant")
                        .WithMany()
                        .HasForeignKey("VariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Variant");
                });

            modelBuilder.Entity("Domain.Entities.VariantValue", b =>
                {
                    b.HasOne("Domain.Entities.Variant", "Variant")
                        .WithMany("VariantValues")
                        .HasForeignKey("VariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Variant");
                });

            modelBuilder.Entity("GalleryProduct", b =>
                {
                    b.HasOne("Domain.Entities.Gallery", null)
                        .WithMany()
                        .HasForeignKey("GalleriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MediaProduct", b =>
                {
                    b.HasOne("Domain.Entities.Media", null)
                        .WithMany()
                        .HasForeignKey("MediasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PermissionRole", b =>
                {
                    b.HasOne("Domain.Entities.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductVariant", b =>
                {
                    b.HasOne("Domain.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Variant", null)
                        .WithMany()
                        .HasForeignKey("VariantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Navigation("Classifications");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("Domain.Entities.Classification", b =>
                {
                    b.Navigation("Options");
                });

            modelBuilder.Entity("Domain.Entities.Gallery", b =>
                {
                    b.Navigation("Medias");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Navigation("ClassificationAttributeValues");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("Domain.Entities.Variant", b =>
                {
                    b.Navigation("VariantValues");
                });

            modelBuilder.Entity("Domain.Entities.VariantItem", b =>
                {
                    b.Navigation("ClassificationAttributeValues");

                    b.Navigation("Galleries");
                });
#pragma warning restore 612, 618
        }
    }
}
