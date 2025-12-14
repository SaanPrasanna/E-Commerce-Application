using E_CommerceApplication.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceApplication.Infrastructure.Data {
    public class ApplicationDbContext : IdentityDbContext<User> {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            // CONFIGURE RELATIONSHOPS AND CONSTRAINTS

            // PRODUCT ENTITY CONFIGURATION
            builder.Entity<Product>(entity => {
                entity.Property(p => p.Price)
                .HasPrecision(18, 2);

                entity.Property(p => p.Discount)
                .HasPrecision(18, 2);

                entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(p => p.SKU)
                .IsUnique();

                entity.HasIndex(p => p.Name);
            });

            // CATEGORY ENTITY CONFIGURATION
            builder.Entity<Category>(entity => {
                entity.HasOne(c => c.ParentCategory)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            // CART ENTITY CONFIGURATION
            builder.Entity<Cart>(entity => {
                entity.HasOne(c => c.User)
                .WithOne(u => u.Cart)
                .HasForeignKey<Cart>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            // CART ITEMS ENTITY CONFIGURATION
            builder.Entity<CartItem>(entity => {
                entity.Property(ci => ci.Price)
                .HasPrecision(18, 2);

                entity.Property(ci => ci.SubTotal)
                .HasPrecision(18, 2);

                entity.HasOne(ci => ci.Cart)
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems);
            });

            // ORDER ENTITY CONFIGURATION
            builder.Entity<Order>(entity => {
                entity.Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

                entity.Property(o => o.ShippingCost)
                .HasPrecision(18, 2);

                entity.Property(o => o.TaxAmount)
                .HasPrecision(18, 2);

                entity.Property(o => o.DiscountAmount)
                .HasPrecision(18, 2);

                entity.Property(o => o.FinalAmount)
                .HasPrecision(18, 2);

                entity.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(o => o.ShippingAddress)
                .WithMany()
                .HasForeignKey(o => o.ShippingAddressId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(o => o.OrderNumber)
                .IsUnique();

                entity.HasIndex(o => o.UserId);
                entity.HasIndex(o => o.Status);
            });

            // ORDER ITEMS ENTITY CONFIGURATION
            builder.Entity<OrderItem>(entity => {
                entity.Property(o => o.UnitPrice)
                .HasPrecision(18, 2);

                entity.Property(o => o.SubTotal)
                .HasPrecision(18, 2);

                entity.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            // REVIEW ENTITY CONFIGURATION
            builder.Entity<Review>(entity => {
                entity.HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(r => new { r.ProductId, r.UserId }).IsUnique();
            });

            // WISHLIST ENTITY CONFIGURATION
            builder.Entity<Wishlist>(entity => {
                entity.HasOne(w => w.User)
                .WithOne(u => u.Wishlist)
                .HasForeignKey<Wishlist>(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(w => w.Products)
                .WithMany()
                .UsingEntity(j => j.ToTable("WishlistProducts"));
            });

            // ADDRESS ENTITY CONFIGURATION
            builder.Entity<Address>(entity => {
                entity.HasOne(a => a.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            // PAYMENT ENTITY CONFIGURATION
            builder.Entity<Payment>(entity => {
                entity.Property(p => p.Amount)
                .HasPrecision(18, 2);

                entity.HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(p => p.TransactionId);
            });
        }
    }
}
