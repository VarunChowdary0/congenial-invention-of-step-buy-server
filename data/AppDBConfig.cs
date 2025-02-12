using Microsoft.EntityFrameworkCore;
using step_buy_server.models;
using step_buy_server.models.Logistics;
using step_buy_server.models.Personal;
using step_buy_server.models.Product_info;

namespace step_buy_server.data;

public class AppDBConfig:DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        DbSet<Product> products = Set<Product>();
        DbSet<User> users = Set<User>();
        DbSet<Feature> features = Set<Feature>();
        DbSet<Address> addresses = Set<Address>();
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        // TPT Configuration: Creates separate tables for CartItem & OrderItem
        modelBuilder.Entity<CartItem>().ToTable("CartItems");
        modelBuilder.Entity<OrderItem>().ToTable("OrderItems");
        
        
        modelBuilder.Entity<Media>()
            .Property(m => m.Type)
            .HasConversion<string>(); // Stores enum as string in DB
    }

}