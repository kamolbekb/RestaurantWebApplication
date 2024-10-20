using Microsoft.EntityFrameworkCore;
using WebRestaurantManagement.Models;

namespace WebRestaurantManagement.Context;

public class MyDbContext : DbContext
{
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Meal> Meals { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderDetail> OrderDetails { get; set; }
    public virtual DbSet<Waiter> Waiters { get; set; }

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id).HasName("pk_categories");
        });

        modelBuilder.Entity<Waiter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_waiters");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_orders");

            entity.HasOne(d => d.Waiter).WithMany(e => e.Orders)
                .HasForeignKey(d => d.WaiterId)
                .HasConstraintName("fk_orders_waiters");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.MealId }).HasName("pk_order_details");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_details_orders");

            entity.HasOne(d => d.Meal).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.MealId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_details_meals");
        });

        modelBuilder.Entity<Meal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_meals");

            entity.HasOne(e => e.Category).WithMany(p => p.Meals)
                .HasForeignKey(e => e.CategoryId)
                .HasConstraintName("fk_meals_categories");
        });
    }
}