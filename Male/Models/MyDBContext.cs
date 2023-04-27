using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace Male.Models;
public class MyDBContext : DbContext
{
    public MyDBContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .Property(b => b.id)
            .HasDefaultValue("newid()");

        modelBuilder.Entity<Account>()
            .HasIndex(b => b.email)
            .IsUnique();


        modelBuilder.Entity<Role>()
            .Property(b => b.id)
            .HasDefaultValue("newid()");

        modelBuilder.Entity<Category>()
           .Property(b => b.id)
           .HasDefaultValue("newid()");

        modelBuilder.Entity<Brand>()
           .Property(b => b.id)
           .HasDefaultValue("newid()");

        modelBuilder.Entity<Product>()
         .Property(b => b.id)
         .HasDefaultValue("newid()");

        modelBuilder.Entity<Cart>()
       .Property(b => b.id)
       .HasDefaultValue("newid()");

        modelBuilder.Entity<Order>()
       .Property(b => b.id)
       .HasDefaultValue("newid()");

        modelBuilder.Entity<Blog>()
         .Property(b => b.id)
         .HasDefaultValue("newid()");

        modelBuilder.Entity<PaymentMethod>()
        .Property(b => b.id)
        .HasDefaultValue("newid()");

        modelBuilder.Entity<OrderPayment>()
       .Property(b => b.id)
       .HasDefaultValue("newid()");
    }




    public DbSet<Account> Accounts { set; get; } = default!;
    public DbSet<Role> Roles { set; get; } = default!;
    public DbSet<Category> Categories { set; get; } = default!;
    public DbSet<Brand> Brands { set; get; } = default!;

    public DbSet<Product> Products { set; get; } = default!;

    public DbSet<Cart> Carts { set; get; } = default!;

    public DbSet<Order> Orders { set; get; } = default!;
    public DbSet<Blog> Blogs { set; get; } = default!;
    public DbSet<PaymentMethod> PaymentMethods { set; get; } = default!;
    public DbSet<OrderPayment> OrderPayments { set; get; } = default!;




}

