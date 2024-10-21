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

    public virtual DbSet<Report> Reports { get; set; }
    public virtual DbSet<Complaint> Complaints { get; set; }
    public virtual DbSet<Recipe> Recipes { get; set; }
    public virtual Menu Menu { get; set; }
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasNoKey();
        });
        
        

        modelBuilder.Entity<Complaint>(entity =>
        {
            entity.HasKey(r => r.Id).HasName("pk_complaints");
            
            entity.HasOne(c=>c.Customer).WithMany(c=>c.Complaints)
                .HasForeignKey(f=>f.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customer_complaints");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(c => c.Id);
        });
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id).HasName("pk_categories");

            entity.HasData(

                new Category { Id = 1, Name = "Appetizers", MealsCount = 2 },
                new Category { Id = 2, Name = "Main Courses", MealsCount = 3 }
            );
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<Waiter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_waiters");

            entity.HasData(
                new Waiter { Id = 1, Name = "John Doe", HireDate = new DateOnly(2020, 1, 10), Address = "123 Main St" },
                new Waiter { Id = 2, Name = "Jane Smith", HireDate = new DateOnly(2019, 5, 20), Address = "456 Elm St" }
            );
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_orders");

            entity.HasOne(d => d.Waiter).WithMany(e => e.Orders)
                .HasForeignKey(d => d.WaiterId)
                .HasConstraintName("fk_orders_waiters");
            
            entity.HasOne(c=>c.Customer)
                .WithMany(o=>o.Orders)
                .HasForeignKey(f=>f.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_customer_orders");

            entity.HasData(
                new Order { Id = 1, OrderDateTime = new DateOnly(2023, 10, 1), WaiterId = 1 },
                new Order { Id = 2, OrderDateTime = new DateOnly(2023, 10, 2), WaiterId = 2 }
            );
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

            entity.HasData(
                new OrderDetail { OrderId = 1, MealId = 1, UnitPrice = 5.50, Count = 2 },
                new OrderDetail { OrderId = 1, MealId = 3, UnitPrice = 12.00, Count = 1 },
                new OrderDetail { OrderId = 2, MealId = 4, UnitPrice = 8.50, Count = 1 },
                new OrderDetail { OrderId = 2, MealId = 5, UnitPrice = 15.00, Count = 2 }
            );
        });
        
        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.MealId }).HasName("pk_recipe");

            entity.HasOne(r=>r.)

            entity.HasOne(d => d.Meal).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.MealId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_details_meals");

            entity.HasData(
                new OrderDetail { OrderId = 1, MealId = 1, UnitPrice = 5.50, Count = 2 },
                new OrderDetail { OrderId = 1, MealId = 3, UnitPrice = 12.00, Count = 1 },
                new OrderDetail { OrderId = 2, MealId = 4, UnitPrice = 8.50, Count = 1 },
                new OrderDetail { OrderId = 2, MealId = 5, UnitPrice = 15.00, Count = 2 }
            );
        });

        modelBuilder.Entity<Meal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_meals");

            entity.HasOne(e => e.Category).WithMany(p => p.Meals)
                .HasForeignKey(e => e.CategoryId)
                .HasConstraintName("fk_meals_categories");

            entity.HasData(
                new Meal { Id = 1, Name = "Bruschetta", Price = 5.50,Describtion = "Grilled bread with toppings", CategoryId = 1 },
                new Meal { Id = 2, Name = "Garlic Bread",Price = 3.50, Describtion = "Toasted bread with garlic", CategoryId = 1 },
                new Meal { Id = 3, Name = "Grilled Chicken",Price = 12.00, Describtion = "Chicken with herbs", CategoryId = 2 },
                new Meal { Id = 4, Name = "Pasta Carbonara",Price = 8.50, Describtion = "Creamy pasta with bacon", CategoryId = 2 },
                new Meal { Id = 5, Name = "Steak",Price = 15.00, Describtion = "Grilled beef steak", CategoryId = 2 }
            );
        });
    }
}