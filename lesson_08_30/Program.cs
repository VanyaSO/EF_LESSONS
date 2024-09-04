using System.ComponentModel.DataAnnotations.Schema;

namespace lesson_08_30;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

class Program
{
    static void Main()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
 
            // List<Company> companies = new List<Company>
            // {
            //     new Company { Name = "Acme Corporation", Address = "123 Main St, Cityville, USA" },
            //     new Company { Name = "Tech Innovators Ltd.", Address = "456 Tech Blvd, Technocity, USA" },
            //     new Company { Name = "Global Logistics Solutions", Address = "789 Logistics Ave, Transportville, USA" },
            //     new Company { Name = "Green Energy Solutions Inc.", Address = "101 Eco Street, Greenburg, USA" },
            //     new Company { Name = "Financial Wizards LLC", Address = "202 Money Lane, Cashville, USA" }
            // };
            //
            // List<User> users = new List<User>
            // {
            //     new User { FirstName = "John", LastName = "Doe", PhoneNumber = "+44-204-577-0077", Passport = "AB569784", Age = 21, Salary = 3500},
            //     // new User { FirstName = "Jane", LastName = "Smith", PhoneNumber = "+1-800-925-6278", Passport = null, Age = 30, Salary = 2600 },
            //     new User { FirstName = "Bob", LastName = "Johnson", PhoneNumber = "+1-800-689-1234", Passport = "CD569784", Age = 40, Salary = 8000 },
            //     new User { FirstName = "Alice", LastName = "Williams", PhoneNumber = "+44-111-899-3127", Passport = "EF569784", Age = 19, Salary = 6500 },
            //     new User { FirstName = "Charlie", LastName = "Brown", PhoneNumber = "+1-800-976-8901", Passport = "GH569784", Age = 26, Salary = 1800 },
            //     new User { FirstName = "Emily", LastName = "Davis", PhoneNumber = "+1-800-632-4567", Passport = "IJ569784", Age = 34, Salary = 2000 },
            //     new User { FirstName = "David", LastName = "Miller", PhoneNumber = "+1-800-519-2345", Passport = "KL569784", Age = 16, Salary = 10_000 },
            //     new User { FirstName = "Sophia", LastName = "Wilson", PhoneNumber = "+1-800-768-9012", Passport = "MN569784", Age = 22, Salary = 6300 },
            //     // new User { FirstName = "Ethan", LastName = "Taylor", PhoneNumber = "+44-204-577-0077", Passport = null, Age = 27, Salary = 3800 },
            //     // new User { FirstName = "Olivia", LastName = "Martin", PhoneNumber = null, Passport = "UV569784", Age = 53, Salary = 2300 },
            //     new User { FirstName = "Tom", LastName = "Berel", PhoneNumber = "+1-800-465-1234", Passport = "ST569784", Age = null, Salary = 2900 },
            //     new User { FirstName = "Kate", LastName = "Filips", PhoneNumber = "+1-800-543-6789", Passport = "WX569784", Age = 62, Salary = 4950 },
            //     new User { FirstName = "Tom", LastName = "Smith", PhoneNumber = "+44-587-365-4488", Passport = "AA569784", Age = 39, Salary = 1900 },
            //     // new User { FirstName = "Alan", LastName = "Dilan", PhoneNumber = null, Passport = "YZ569784", Age = 44, Salary = 2450 },
            //  };
 
            // db.Companies.AddRange(companies);
            // db.Users.AddRange(users);
            // db.Workers.Add(new Worker
            // {
                // Age = 19,
                // Position = "De"
            // });
            db.SaveChanges();
        }
        
        using (ApplicationContext db = new ApplicationContext())
        {
            
        }
    }
}
// public class Company
// {
//     [Column("MyCompanyId")] 
//     public int Id { get; set; }
//     public string? Name { get; set; }
//     public string? Address { get; set; }
// }
//  
// public class User
// {
//     
//     public int Id { get; set; }
//     public string? FirstName { get; set; }
//     public string? LastName { get; set; }
//     public string? Fio { get; set; }
//     public string? PhoneNumber { get; set; }
//     public string? Passport { get; set; }
//     public int? Age { get; set; }
//     public bool IsPermanent { get; set; } = false;
//     public decimal Salary { get; set; }
//     // public DateTime DateTime { get; set; }
//     // public int Year { get; set; }
// }

// практика
// public class Worker
// {
    // public int WorkerId { get; set; }
    // public string? FirstName { get; set; }
    // public string? LastName { get; set; }
    // public string? Position { get; set; }
    // public int Age { get; set; }
// }

//правтика 2

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    
    public Product(int id, string name, decimal price, string category, string description)
    {
        Id = id;
        Name = name;
        Price = price;
        Category = category;
        Description = description;
    }
}

public class ApplicationContext : DbContext
{
    // практика 
    // public DbSet<Worker> Workers { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    
    // public DbSet<User> Users { get; set; } = null!;
    // public DbSet<Company> Companies { get; set; } = null!;
    
 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string str = "Server=localhost;Database=efLessons;User Id=sa;Password=admin@Admin87457;TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(str);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // практика 
        // modelBuilder.Entity<Worker>(e =>
        // {
            // Составной ключ на 2 столбца.
            // e.HasKey(e => new
            // {
            //     e.FirstName,
            //     e.LastName
            // });
            
            // Ограничение длины для строкового столбца.
            // e.Property(e => e.Position).HasMaxLength(30);
            
            //Атрибут CHECK для возраста.
            // e.ToTable(e => e.HasCheckConstraint("Age", "Age >= 18"));
            
            // Атрибут CHECK для должности работника (или другое с использование перечисления (Enum)).
            // e.ToTable(e => e.HasCheckConstraint("CHK_Position", "Position IN ('Manager', 'Dev')"));
            
            // Указание другого имени ключа для таблицы (В классе использовать Id, для таблицы к примеру - ProductId).
            // e.HasKey(e => e.WorkerId);
            
            // Использовать обязательные и не обязательные свойства (с атрибутом Required).
            // e.Property(e => e.Position).IsRequired();
        // });
        
        
        // -------------------------------------
        
        
        //правтика 2

        modelBuilder.Entity<Product>(e =>
        {
            // Убедитесь, что свойства имеют соответствующие типы данных в базе данных. 
            e.Property(e => e.Name).HasColumnType("nvarchar(100)");
            e.Property(e => e.Price).HasColumnType("decimal");
            e.Property(e => e.Category).HasColumnType("nvarchar(max)");
            e.Property(e => e.Description).HasColumnType("nvarchar(max)");
            
            // Для принятия данных, определите конструктор.
            // +
                
            // Используйте составной ключ.
            

            // Установите ограничение, чтобы название продукта (Name) было обязательным и не превышало 100 символов. 

            // Установите ограничение, чтобы цена продукта (Price) была больше 0. 

            // Вместо одного свойства, используйте поле.

            // При необходимости выполните другие дополнительные настройки модели данных, которые могут потребоваться для вашего приложения учета продуктов.

        });


        // поменять имя первичного ключа для user 
        // modelBuilder.Entity<User>().HasKey(x=>x.UserKey);

        // включить балицу не имея DbSet<Company>
        // modelBuilder.Entity<Company>();
        // base.OnModelCreating(modelBuilder);

        // игнорировать включение таблицы
        // modelBuilder.Ignore<Company>();

        // поменять тип Id вместо string на Guid
        // modelBuilder.Entity<User>().HasKey(x=>x.Id);

        // тип id string - обьязательно и генерируется на строне сервера 
        // modelBuilder.Entity<User>().Property(x => x.Id).IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");

        // исключения свойства
        // modelBuilder.Entity<User>().Ignore(x => x.Age);

        // назвать таблицу другим именнем при создании не как в DbSet
        // modelBuilder.Entity<User>().ToTable("People");

        // переимновать столбцы еще иможно но я не успел записать 

        // сделать столбик обезательным в таблице
        // modelBuilder.Entity<User>().Property(x => x.PhoneNumber).IsRequired();

        // составные ключи
        // modelBuilder.Entity<User>().HasKey(e => new
        // {
        // e.Passport,
        // e.PhoneNumber
        // });

        // альтернативные ключи
        // modelBuilder.Entity<User>().HasAlternateKey(e => e.Passport);

        // навешивание индексов index и дать имя индексу PassportIsUnique
        // modelBuilder.Entity<User>().HasIndex(e => e.Passport).IsUnique().HasDatabaseName("PassportIsUnique");

        // не генерировать автоматически ключи
        // modelBuilder.Entity<User>().Property(e => e.Id).ValueGeneratedNever();

        // установить значение по умолчанию
        // modelBuilder.Entity<User>().Property(e => e.Age).HasDefaultValue(18);

        // установить значение по умолчанию на сервере вызвав метода на нём
        // modelBuilder.Entity<User>().Property(e => e.DateTime).HasDefaultValueSql("GETDATE()");

        // создавать новое поле исходя их двугих 
        // modelBuilder.Entity<User>().Property(e => e.Fio).HasComputedColumnSql("FirstName + ' ' + LastName ");

        // modelBuilder.Entity<User>().Property(e => e.Year).HasComputedColumnSql("DATEPART(Year, GETDATE()) - Age");

        // modelBuilder.Entity<User>().ToTable(e => e.HasCheckConstraint("Age", "Age > 0 and Age < 150"));

        // modelBuilder.Entity<User>().Property(e => e.FirstName).HasMaxLength(60);

        // добавляет данные в таблицу во время миграции тоесть заложен в код миграции или сразу после создания бд 
        // modelBuilder.Entity<User>().HasData(
        // new User { Id = 1, FirstName = "Mark", LastName = "Wilson", PhoneNumber = "+1-800-768-9012", Passport = "MN569784", Age = 22, Salary = 6300 }
        // );

        // можно групировать
        // modelBuilder.Entity<User>(e =>
        // {
        // e.HasAlternateKey(e => e.Passport);
        // e.HasData(
        // new User
        // {
        // Id = 1, FirstName = "Mark", LastName = "Wilson", PhoneNumber = "+1-800-768-9012",
        // Passport = "MN569784", Age = 22, Salary = 6300
        // }
        // );
        //...
        // });
    }
}


// Разработайте приложения для учета продуктов в интернет-магазине. Вам необходимо создать модель данных для таблицы «Продукты». У продукта должны быть следующие свойства: 
 
// Идентификатор (Id) 
// Название (Name) 
// Цена (Price) 
// Категория (Category) 
// Описание (Description) 
 
// Ваша задача состоит в следующем. Используя Fluent API в EF Core, создайте модель данных для таблицы «Продукты» согласно требованиям ниже:
 
// Убедитесь, что свойства имеют соответствующие типы данных в базе данных. 
    // Для принятия данных, определите конструктор.
    // Используйте составной ключ.
 
    // Установите ограничение, чтобы название продукта (Name) было обязательным и не превышало 100 символов. 
 
    // Установите ограничение, чтобы цена продукта (Price) была больше 0. 
 
    // Вместо одного свойства, используйте поле.
 
    // При необходимости выполните другие дополнительные настройки модели данных, которые могут потребоваться для вашего приложения учета продуктов.