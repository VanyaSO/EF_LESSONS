using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace lesson_09_04;

class Program
{
    static void Main()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            Country Ukraine = new Country { Name = "Ukraine" };
            Country USA = new Country { Name = "USA" };

            Airplane airp1 = new Airplane { Name = "Plane1", AirplaneSettings = new AirplaneCharacteristics{NumberSeats = 32}};
            Airplane airp2 = new Airplane { Name = "Plane2", AirplaneSettings = new AirplaneCharacteristics{NumberSeats = 2} };
            Airplane airp3 = new Airplane { Name = "Plane3", AirplaneSettings = new AirplaneCharacteristics{NumberSeats = 7}};
            
            Airport Ap1 = new Airport { Name = "Airport 1", Airplanes = new() {airp1, airp2}, Country = Ukraine};
            Airport Ap2 = new Airport { Name = "Airport 2", Airplanes = new() {airp1, airp3}, Country = USA};
            
            db.Airports.AddRange(Ap1, Ap2);
            db.SaveChanges();
            
            // Реализовать возможность получение полных данных, а самолете
            // (сам самолет, его характеристики, аэропорт в котором он находится, и страна в которой находится аэропорт
            var aboutPlane = db.Airplanes
                .Include(a => a.AirplaneSettings)
                .Include(a => a.Airports)
                .ThenInclude(air => air.Country)
                .ToList();
        }
    }

    class ApplicationContext : DbContext
    {
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<Airport> Airports { get; set; } = null!;
        public DbSet<Airplane> Airplanes { get; set; } = null!;
        public DbSet<AirplaneCharacteristics> AirplaneCharacteristics { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        
            optionsBuilder.UseSqlServer("Server=localhost;Database=CW;User=sa;Password=admin@Admin87457; TrustServerCertificate = True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }

    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Airport> Airports { get; set; } = new ();
    }

    public class Airport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Airplane> Airplanes { get; set; } = new ();
        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
    
    public class Airplane
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public AirplaneCharacteristics AirplaneSettings { get; set; }
        public List<Airport> Airports { get; set; } = new();
    }

    public class AirplaneCharacteristics
    {
        public int Id { get; set; }
        // public string Color { get; set; }
        public int NumberSeats { get; set; }
        public int AirplaneId { get; set; }
        public Airplane Airplane { get; set; }
    }
};



/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
// using System.ComponentModel.DataAnnotations.Schema;
// using Microsoft.EntityFrameworkCore;
//
// class Program
// {
//     static void Main()
//     {
//         using (ApplicationContext db = new ApplicationContext())
//         {
//             db.Database.EnsureDeleted();
//             db.Database.EnsureCreated();
//             List<User> users = new List<User>()
//             {
//                 new User
//                 {
//                     Name = "Alex",
//                     Language = new Language
//                     {
//                         Name = "English",
//                         LanguageDetails = new LanguageDetails
//                         {
//                             LanguageLevel = LanguageLevel.A2,
//                             TrainingStartDate = new DateOnly(2023, 05, 14)
//                         }
//                     }
//                 },
//
//                 new User
//                 {
//                     Name = "Marry",
//                     Language = new Language
//                     {
//                         Name = "English",
//                         LanguageDetails = new LanguageDetails
//                         {
//                             LanguageLevel = LanguageLevel.C1,
//                             TrainingStartDate = new DateOnly(2020, 01, 10)
//                         }
//                     }
//                 }
//             };
//
//             db.Users.AddRange(users);
//             db.SaveChanges();
//         }
//     }
// }
//
// public class ApplicationContext : DbContext
// {
//     public DbSet<User> Users { get; set; }
//
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         optionsBuilder.UseSqlServer(
//             "Server=localhost;Database=CW;User=sa;Password=admin@Admin87457; TrustServerCertificate = True;");
//     }
//
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<User>(e => e.ComplexProperty(
//             e => e.Language,
//             b => { b.ComplexProperty(e => e.LanguageDetails); }
//         ));
//         base.OnModelCreating(modelBuilder);
//     }
// }
//
// public class User
// {
//     public int Id { get; set; }
//     public string Name { get; set; }
//     public Language Language { get; set; }
// }
//
// public enum LanguageLevel
// {
//     A1,
//     A2,
//     B1,
//     B2,
//     C1,
//     C2
// }
//
// [ComplexType]
// public class LanguageDetails
// {
//     public DateOnly TrainingStartDate { get; set; }
//     public DateOnly? TrainingEndDate { get; set; }
//     public LanguageLevel LanguageLevel { get; set; }
// }
//
// [ComplexType]
// public class Language
// {
//     public required string Name { get; set; }
//     public required LanguageDetails LanguageDetails { get; set; }
// }

// using Microsoft.EntityFrameworkCore;
//
// namespace lesson_09_04;
//
// class Program
// {
//     static void Main()
//     {
//         using (ApplicationContext db = new ApplicationContext())
//         {
//             db.Database.EnsureDeleted();
//             db.Database.EnsureCreated();
//             db.SaveChanges();
//         }
//     }
//
//     class ApplicationContext : DbContext
//     {
//         public DbSet<Customer> Customers { get; set; }
//         public DbSet<Order> Orders { get; set; }
//         
//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             optionsBuilder.UseSqlServer("Server=localhost;Database=CW;User=sa;Password=admin@Admin87457; TrustServerCertificate = True;");
//         }
//
//         protected override void OnModelCreating(ModelBuilder modelBuilder)
//         {
//             modelBuilder.Entity<Customer>().ComplexProperty(e => e.Address);
//  
//             modelBuilder.Entity<Order>(b =>
//             {
//                 b.ComplexProperty(e => e.BillingAddress);
//                 b.ComplexProperty(e => e.ShippingAddress);
//             });
//         }
//     }
//
//     public class Address
//     {
//         public string Country { get; set; }
//         public string City { get; set; }
//         public string PostCode { get; set; }
//         public string Line1 { get; set; }
//         public string? Line2 { get; set; }
//     }
//
//     public class Customer
//     {
//         public int Id { get; set; }
//         public string Name { get; set; }
//         public Address Address { get; set; }
//         public List<Order> Orders { get; } = new();
//     }
//  
//     public class Order
//     {
//         public int Id { get; set; }
//         public string Contents { get; set; }
//         public Address ShippingAddress { get; set; }
//         public Address BillingAddress { get; set; }
//         public Customer Customer { get; set; } = null!;
//     }
// }


//////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////
// using Microsoft.EntityFrameworkCore;
//
// namespace lesson_09_04;
//
// class Program
// {
//     static void Main()
//
//     {
//         List<Ingredient> ingredients = new List<Ingredient>()
//         {
//             new Ingredient { Name = "Water" },
//             new Ingredient { Name = "Apple" },
//             new Ingredient { Name = "Citrus" },
//             new Ingredient { Name = "Egg" },
//             new Ingredient { Name = "Flour" },
//             new Ingredient { Name = "Salt" },
//             new Ingredient { Name = "Opener" },
//             new Ingredient { Name = "Cherry" }
//         };
//
//         using (ApplicationContext db = new ApplicationContext())
//         {
//             db.Database.EnsureDeleted();
//             db.Database.EnsureCreated();
//             db.Ingredients.AddRange(ingredients);
//             db.SaveChanges();
//
//             Dish applePie = new Dish
//
//             {
//                 Name = "Apple Pie",
//
//                 Price = 100,
//             };
//
//             Dish cherryPie = new Dish
//             {
//                 Name = "Cherry Pie",
//                 Price = 110,
//             };
//
//             List<IngredientInDish> ingredientsInDishes = new List<IngredientInDish>()
//             {
//                 //For apple pie
//                 new IngredientInDish { Dish = applePie, Ingredient = ingredients[0], Commensurate = Commensurate.milliliter, Count = 750 },
//                 new IngredientInDish { Dish = applePie, Ingredient = ingredients[1], Commensurate = Commensurate.pieces, Count = 5 },
//                 new IngredientInDish { Dish = applePie, Ingredient = ingredients[2], Commensurate = Commensurate.pieces, Count = 1 },
//                 new IngredientInDish { Dish = applePie, Ingredient = ingredients[3], Commensurate = Commensurate.milliliter, Count = 2 },
//                 new IngredientInDish { Dish = applePie, Ingredient = ingredients[4], Commensurate = Commensurate.gram, Count = 250 },
//                 new IngredientInDish { Dish = applePie, Ingredient = ingredients[5], Commensurate = Commensurate.pinch, Count = 1 }, 
//                 new IngredientInDish { Dish = applePie, Ingredient = ingredients[6], Commensurate = Commensurate.teaspoon, Count = 1 },
//                 //For cherry pie
//                 new IngredientInDish { Dish = cherryPie, Ingredient = ingredients[0], Commensurate = Commensurate.milliliter, Count = 750 },
//                 new IngredientInDish { Dish = cherryPie, Ingredient = ingredients[7], Commensurate = Commensurate.pieces, Count = 5 },
//                 new IngredientInDish { Dish = cherryPie, Ingredient = ingredients[2], Commensurate = Commensurate.pieces, Count = 1 }, 
//                 new IngredientInDish { Dish = cherryPie, Ingredient = ingredients[3], Commensurate = Commensurate.milliliter, Count = 2 },
//                 new IngredientInDish { Dish = cherryPie, Ingredient = ingredients[4], Commensurate = Commensurate.gram, Count = 250 },
//                 new IngredientInDish { Dish = cherryPie, Ingredient = ingredients[5], Commensurate = Commensurate.pinch, Count = 1 },
//                 new IngredientInDish { Dish = cherryPie, Ingredient = ingredients[6], Commensurate = Commensurate.teaspoon, Count = 1 }
//             };
//
//             db.Dishes.AddRange(applePie, cherryPie);
//             db.IngredientInDishes.AddRange(ingredientsInDishes);
//             db.SaveChanges();
//         }
//     }
//
//     class ApplicationContext : DbContext
//     {
//         public DbSet<Dish> Dishes { get; set; }
//         public DbSet<Ingredient> Ingredients { get; set; }
//         public DbSet<IngredientInDish> IngredientInDishes { get; set; }
//
//         public ApplicationContext()
//         {
//             Database.EnsureCreated();
//         }
//
//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             optionsBuilder.UseSqlServer(
//                 "Server=localhost;Database=CW;User=sa;Password=admin@Admin87457; TrustServerCertificate = True;");
//         }
//
//         protected override void OnModelCreating(ModelBuilder modelBuilder)
//         {
//             modelBuilder.Entity<Dish>().HasMany(c => c.Ingredients).WithMany(s => s.Dishes)
//                 .UsingEntity<IngredientInDish>(
//                     e => e.HasOne(e => e.Ingredient).WithMany(s => s.IngredientInDishes)
//                         .HasForeignKey(e => e.IngredientId),
//                     e => e.HasOne(e => e.Dish).WithMany(c => c.IngredientInDishes).HasForeignKey(e => e.DishId),
//                     e =>
//                     {
//                         e.Property(e => e.Count);
//                         e.Property(e => e.Commensurate);
//                         e.HasKey(e => e.Id);
//                         e.ToTable("IngredientInDish");
//                     });
//         }
//     }
//
//     public enum Commensurate
//     {
//         gram = 1,
//         kilo,
//         milliliter,
//         pieces,
//         pinch,
//         teaspoon
//     }
//
//     public class Dish
//     {
//         public int Id { get; set; }
//         public string Name { get; set; }
//         public decimal Price { get; set; }
//
//         public virtual IEnumerable<Ingredient> Ingredients { get; set; }
//         public virtual IEnumerable<IngredientInDish> IngredientInDishes { get; set; }
//     }
//
//     public class Ingredient
//     {
//         public int Id { get; set; }
//         public string Name { get; set; }
//         public virtual IEnumerable<Dish> Dishes { get; set; }
//         public virtual IEnumerable<IngredientInDish> IngredientInDishes { get; set; }
//     }
//
//     public class IngredientInDish
//     {
//         public int Id { get; set; }
//         public int IngredientId { get; set; }
//         public int DishId { get; set; }
//         public int Count { get; set; }
//         public Commensurate Commensurate { get; set; }
//
//         public Ingredient Ingredient { get; set; }
//         public Dish Dish { get; set; }
//     }
// }