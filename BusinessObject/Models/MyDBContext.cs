using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Models
{
    public class MyDBContext : IdentityDbContext<ApplicationUser>
    {
        protected MyDBContext()
        {
        }

        public MyDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyStoreDB"));
        }
        
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
                {
                    entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B33ACDDF6");

                    entity.ToTable("Category");

                    entity.Property(e => e.CategoryId).ValueGeneratedNever();
                    entity.Property(e => e.CategoryName)
                        .HasMaxLength(40)
                        .IsUnicode(false);
                    entity.HasData(
                    new Category { CategoryId = 1, CategoryName = "Beverages" },
                    new Category { CategoryId = 2, CategoryName = "Condiments" },
                    new Category { CategoryId = 3, CategoryName = "Confections" },
                    new Category { CategoryId = 4, CategoryName = "Dairy Products" },
                    new Category { CategoryId = 5, CategoryName = "Grains/Cereals" },
                    new Category { CategoryId = 6, CategoryName = "Meat/Poulty" },
                    new Category { CategoryId = 7, CategoryName = "Produce" },
                    new Category { CategoryId = 8, CategoryName = "Seafood" }
                    );
                }
            );
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId).HasName("PK__Order__C3905BCFA94A0A60");

                entity.ToTable("Order");

                entity.Property(e => e.OrderId).ValueGeneratedNever();
                entity.Property(e => e.Freight).HasColumnType("money");
                entity.Property(e => e.OrderDate).HasColumnType("datetime");
                entity.Property(e => e.RequiredDate).HasColumnType("datetime");
                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

                entity.HasOne(d=>d.User).WithMany(p=>p.Orders).HasForeignKey(e => e.MemberId);
            });
            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("PK__OrderDet__08D097A31E6530A4");

                entity.ToTable("OrderDetail");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderDeta__Order__2E1BDC42");

                entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__OrderDeta__Produ__2F10007B");
            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CD112E0C4B");

                entity.ToTable("Product");

                entity.Property(e => e.ProductId).ValueGeneratedNever();
                entity.Property(e => e.ProductName)
                    .HasMaxLength(40)
                    .IsUnicode(false);
                entity.Property(e => e.UnitPrice).HasColumnType("money");
                entity.Property(e => e.Weight)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category).WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Product__Categor__2B3F6F97");
            });
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
