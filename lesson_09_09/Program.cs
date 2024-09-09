using Microsoft.EntityFrameworkCore;

namespace lesson_09_09;

class Program
{
    static void Main()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }

        DbService service = new DbService();

        Order order = new Order { };
        service.AddOrder(order);
        
        Delivery delivery = new Delivery
        {
            Fio = "MyFio",
            Address = "MyAdsress",
            PaymentType = PaymentType.Cart,
            StatusDelivery = StatusDelivery.Processing,
            Details = "MyDetails",
            DateOfDispatch = DateTime.Now,
            DateOfReceipt = DateTime.Today,
            UserId = 1,
            OrderId = order.Id
        };
        service.AddDelivery(delivery);
    }
};

public class DbService
{
    public Product? GetProduct(int id)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            return db.Products
                .Include(e => e.Category)
                .Include(e => e.Orders)
                .FirstOrDefault(e => e.Id == id);
        }
    }
    public void AddOrder(Order order)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Orders.Add(order);
            db.SaveChanges(); 
        }
    }
    
    public void AddDelivery(Delivery delivery)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Deliveries.Add(delivery);
            db.SaveChanges();
        }
    }
    
    public void RemoveOrder(Order order)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Orders.Remove(order);
            db.SaveChanges(); 
        }
    }
    
    public void RemoveDelivery(Delivery delivery)
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Deliveries.Remove(delivery);
            db.SaveChanges(); 
        }
    }
}

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=CW;User=sa;Password=admin@Admin87457;TrustServerCertificate=True;");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasMany(s => s.Orders).WithMany(p => p.Products).UsingEntity<OrderProduct>(
            e => e.HasOne(e => e.Order).WithMany(s => s.OrderProducts).HasForeignKey(e => e.OrderId),
            e => e.HasOne(e => e.Product).WithMany(s => s.OrderProducts).HasForeignKey(e => e.ProductId),
            e =>
            {
                e.ToTable("OrderProducts");
            }
        );
        
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Email = "john.doe@example.com", Password = "hashedpassword1" },
            new User { Id = 2, Email = "jane.smith@example.com", Password = "hashedpassword2" }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Men's Fragrances", Description = "Perfumes for men" },
            new Category { Id = 2, Name = "Women's Fragrances", Description = "Perfumes for women" },
            new Category { Id = 3, Name = "Unisex Fragrances", Description = "Perfumes for everyone" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Cool Water",
                Description = "A fresh aquatic scent for men.",
                PurchasePrice = 40.00,
                RetailPrice = 60.00,
                Quantity = 100,
                Manufacturer = "Davidoff",
                ExpirationDate = new DateTime(2025, 12, 31),
                CategoryId = 1
            },
            new Product
            {
                Id = 2,
                Name = "Chanel No. 5",
                Description = "A timeless classic for women.",
                PurchasePrice = 80.00,
                RetailPrice = 120.00,
                Quantity = 50,
                Manufacturer = "Chanel",
                ExpirationDate = new DateTime(2026, 11, 30),
                CategoryId = 2
            },
            new Product
            {
                Id = 3,
                Name = "Tom Ford Black Orchid",
                Description = "A luxurious unisex fragrance.",
                PurchasePrice = 100.00,
                RetailPrice = 150.00,
                Quantity = 30,
                Manufacturer = "Tom Ford",
                ExpirationDate = new DateTime(2026, 5, 15),
                CategoryId = 3
            }
        );
    }
}

// 1) Категории: Название, Описание.
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public List<Product> Products { get; set; }
}
// 2) Товары: Название, описание, закупочная цена, розничная цена, количество, производитель, срок годности. 
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public double PurchasePrice { get; set; }
    public double RetailPrice { get; set; }
    public int Quantity { get; set; }
    public string Manufacturer { get; set; }
    public DateTime ExpirationDate { get; set; }
    public int CategoryId { get; set; }
    public List<Order> Orders  { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
    public Category Category { get; set; }
}

public class Order
{
    public int Id { get; set; }
    public List<Product> Products { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
}

public class OrderProduct
{
    public int OrderId { get; set; }
    public Order Order { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; }
}

// 3) Доставка: Фио, адрес, тип платежа, статус, реквизиты, дата отправки, дата получения. 
public class Delivery
{
    public int Id { get; set; }
    public string Fio { get; set; }
    public string Address { get; set; }
    public PaymentType PaymentType { get; set; }
    public StatusDelivery StatusDelivery { get; set; }
    public string Details { get; set; }
    public DateTime DateOfDispatch { get; set; }
    public DateTime DateOfReceipt { get; set; }
    public int UserId { get; set; }
    public int OrderId { get; set; }
    public User User { get; set; }
    public Product Product { get; set; }
}

public enum PaymentType
{
    Cart,
    UponReceipt
}

public enum StatusDelivery
{
    Processing,
    Sent,
    Arrived,
    Received
}

// 4) Пользователи: Все данные по доставке, email, пароль. Возможность просмотров и удаление заказов.
public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<Delivery> Deliveries { get; set; }
}