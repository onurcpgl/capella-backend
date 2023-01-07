using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class CapellaDbContext : DbContext
    {
        public CapellaDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Classification> Classifications { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<ClassificationAttributeValue> ClassificationAttributeValues { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<MediaFormat> MediaFormats { get; set; }
        public DbSet<Options> Options { get; set; }
        public DbSet<Variant> Variants { get; set; }
        public DbSet<VariantValue> VariantValues { get; set; }
        public DbSet<VariantItem> VariantItems { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Category>()
                .HasOne(category => category.ParentCategory)
                .WithMany(category => category.SubCategories)
                .HasForeignKey(category => category.ParentCategoryId);

            modelBuilder
               .Entity<Product>()
               .HasOne(product => product.Brand)
               .WithMany(brand => brand.Products);

            modelBuilder
              .Entity<Product>()
              .HasMany(product => product.Tags)
              .WithMany(tag => tag.Products)
              .UsingEntity(j => j.ToTable("ProductsTags"));

            modelBuilder
               .Entity<Product>()
               .HasMany(product => product.VariantItems)
               .WithMany(variantItem => variantItem.Products)
               .UsingEntity(j => j.ToTable("ProductsVariantItems"));

            modelBuilder
             .Entity<Variant>()
             .HasMany(variant => variant.VariantValues)
             .WithOne(variantValue => variantValue.Variant);

            modelBuilder
             .Entity<VariantItem>()
             .HasMany(variantItem => variantItem.VariantValues)
             .WithMany(variantValue => variantValue.VariantItems)
             .UsingEntity(j => j.ToTable("VariantValuesVariantItems"));

            modelBuilder
            .Entity<VariantItem>()
            .HasMany(variantItem => variantItem.ClassificationAttributeValues)
            .WithMany(classificationAttributeValue => classificationAttributeValue.VariantItems)
            .UsingEntity(j => j.ToTable("ClassificationAttributeValuesVariantItems"));

            modelBuilder
               .Entity<VariantItem>()
               .HasMany(variantItem => variantItem.Galleries)
               .WithMany(galllery => galllery.VariantItems)
               .UsingEntity(j => j.ToTable("GalleriesVariantItems"));

            modelBuilder
                .Entity<Product>()
                .HasMany(product=> product.Categories)
                .WithMany(category => category.Products)
                .UsingEntity(j => j.ToTable("ProductsCategories"));

            modelBuilder
                .Entity<Classification>()
                .HasMany(c => c.Options)
                .WithOne(o => o.Classification);

            modelBuilder
                .Entity<Product>()
                .HasMany(c => c.ClassificationAttributeValues)
                .WithMany(p => p.Products)
                .UsingEntity(j => j.ToTable("ProductsClassificationAttributeValues"));

            modelBuilder
                .Entity<ClassificationAttributeValue>()
                .HasMany(attributeValue => attributeValue.Options)
                .WithMany(option => option.ClassificationAttributeValues)
                .UsingEntity(j => j.ToTable("ClassificationAttributeValuesOptions"));

            modelBuilder
                .Entity<Product>()
                .HasMany(p => p.Medias)
                .WithMany(m => m.Products)
                .UsingEntity(j => j.ToTable("ProductsMedias"));

          

            modelBuilder
                .Entity<User>()
                .HasMany(user => user.Addresses)
                .WithOne(address => address.User);

            modelBuilder
               .Entity<User>()
               .HasMany(user => user.Roles)
               .WithMany(role => role.Users)
               .UsingEntity(j => j.ToTable("UserRoles"));

            modelBuilder
                .Entity<Role>()
                .HasMany(role => role.Permissions)
                .WithMany(permission => permission.Roles)
                .UsingEntity(crossTable => crossTable.ToTable("RolesPermissions"));

        }
    }
}
