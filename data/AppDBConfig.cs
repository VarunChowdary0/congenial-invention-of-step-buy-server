using Microsoft.EntityFrameworkCore;
using step_buy_server.models;
using step_buy_server.models.Logistics;
using step_buy_server.models.Personal;
using step_buy_server.models.Product_info;
using step_buy_server.models.support;

namespace step_buy_server.data;

public class AppDBConfig:DbContext
{
   
    public AppDBConfig(DbContextOptions<AppDBConfig> options) : base(options) { }
    
    // - Product
    public DbSet<Product> Products { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<Media> Media { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Review> Reviews { get; set; }
    // ---------------------------------------------------
    
    // - Personal
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<AuthentiData> AuthentiDatas { get; set; }
    // --------------------------------------------------
    
    // - Losistics
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }
    public DbSet<DeliveryInstructions> DeliveryInstructions { get; set; }
    public DbSet<Bill> Bills { get; set; }
    // --------------------------------------------------
    
    // - Support
    public DbSet<Support> Supports { get; set; }
    public DbSet<Reports> Reports { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TPT Configuration: Creates separate tables for CartItem & OrderItem
        modelBuilder.Entity<CartItem>().ToTable("CartItems");
        modelBuilder.Entity<OrderItem>().ToTable("OrderItems");
     
        modelBuilder.Entity<Media>()
            .Property(m => m.Type)
            .HasConversion<string>(); // Stores enum as string in DB
        
        modelBuilder.Entity<ProductCategory>()
            .HasKey(pc => new { pc.CategoryId, pc.ProductId }); // composite key for n:n
        
        // relationship between ProductCategory and Product
        // relationship between ProductCategory and Product
        modelBuilder.Entity<ProductCategory>()
            .HasOne(pc => pc.Product)
            .WithMany(p => p.ProductCategories)
            .HasForeignKey(pc => pc.ProductId); // Laptop → (Gaming, Electronics, Office)
        
        // relationship between ProductCategory and Category 
        
        modelBuilder.Entity<ProductCategory>()
            .HasOne(pc => pc.Category)
            .WithMany(c => c.ProductCategories)
            .HasForeignKey(pc => pc.CategoryId); // Gaming → (Laptop, Mouse, Keyboard)


        modelBuilder.Entity<DeliveryInstructions>()
            .HasNoKey();

        // modelBuilder.Entity<Product>()
        //     .HasMany<Feature>();
        //
        // modelBuilder.Entity<Product>()
        //     .HasMany<Media>();
        //
        // modelBuilder.Entity<Product>()
        //     .HasMany<Review>();
    }

}