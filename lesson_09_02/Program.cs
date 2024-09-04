using Microsoft.EntityFrameworkCore;

namespace lesson_09_02;
class Program
{
    static void Main()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
         
            List<Company> companies = new List<Company>
            {
                new Company { Name = "Acme Corporation", Address = "123 Main St, Cityville, USA" },
                new Company { Name = "Tech Innovators Ltd.", Address = "456 Tech Blvd, Technocity, USA" },
                new Company { Name = "Global Logistics Solutions", Address = "789 Logistics Ave, Transportville, USA" },
                new Company { Name = "Green Energy Solutions Inc.", Address = "101 Eco Street, Greenburg, USA" },
                new Company { Name = "Financial Wizards LLC", Address = "202 Money Lane, Cashville, USA" }
            };

            db.Companies.AddRange(companies);
            db.SaveChanges();

            List<Shop> shops = new List<Shop>
            {
                new Shop { Name = "Main Shop", CompanyId = companies[0].Id },
                new Shop { Name = "Tech Shop", CompanyId = companies[1].Id },
                new Shop { Name = "Logistics Shop", CompanyId = companies[2].Id },
                new Shop { Name = "Green Shop", CompanyId = companies[3].Id },
                new Shop { Name = "Financial Shop", CompanyId = companies[4].Id }
            };

            db.Shops.AddRange(shops);
            db.SaveChanges();

            List<User> users = new List<User>
            {
                new User { FirstName = "John", LastName = "Doe", PhoneNumber = "+44-204-577-0077", Passport = "AB569784", Age = 21, Salary = 3500, Shops = new List<Shop>{shops[0], shops[1]} },
                new User { FirstName = "Jane", LastName = "Smith", PhoneNumber = "+1-800-925-6278", Passport = null, Age = 30, Salary = 2600, Shops = new List<Shop>{shops[0], shops[1]} },
                new User { FirstName = "Bob", LastName = "Johnson", PhoneNumber = "+1-800-689-1234", Passport = "CD569784", Age = 40, Salary = 8000, Shops = new List<Shop>{shops[0], shops[1]} },
                new User { FirstName = "Alice", LastName = "Williams", PhoneNumber = "+44-111-899-3127", Passport = "EF569784", Age = 19, Salary = 6500, Shops = new List<Shop>{shops[0], shops[1]} },
                new User { FirstName = "Charlie", LastName = "Brown", PhoneNumber = "+1-800-976-8901", Passport = "GH569784", Age = 26, Salary = 1800, Shops = new List<Shop>{shops[0], shops[1]} },
                new User { FirstName = "Emily", LastName = "Davis", PhoneNumber = "+1-800-632-4567", Passport = "IJ569784", Age = 34, Salary = 2000, Shops = new List<Shop>{shops[0], shops[1]} },
            };

            db.Users.AddRange(users);
            db.SaveChanges();
        }

        using (ApplicationContext db = new ApplicationContext())
        {
            var companies = db.Companies
                .Include(c => c.Shops)
                .ThenInclude(s => s.Users)
                .ToList();
            
            foreach (var company in companies)
            {
                Console.WriteLine(company.Name);
                foreach (var shop in company.Shops)
                {
                    Console.WriteLine(shop.Name);
                    foreach (var user in shop.Users)
                    {
                        Console.WriteLine($"{user.FirstName} {user.LastName}, {user.PhoneNumber}, {user.Passport}, {user.Age}, {user.Salary}");
                    }
                }

            }
        }
        
    }
}

public class Company
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Address { get; set; }
    public List<Shop> Shops { get; set; } = new List<Shop>();
}
public class Shop
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CompanyId { get; set; }

    public Company Company { get; set; }
    public List<User> Users { get; set; } = new List<User>();
}

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<Shop> Shops { get; set; } = null!;
 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=CH;User=sa;Password=admin@Admin87457; TrustServerCertificate = True;");
    }
}


 