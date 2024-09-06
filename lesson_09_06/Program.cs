// Создайте базу данных для управления книгами, авторами и жанрами книг. Определите 3 класса: «Book», «Author, «Genre». 
// Настройте связи между классами, используя Fluent Api. Заполните их начальными данными с использованием методов AddRange() - для добавления элементов и Any() – для проверки наличия.
// 	
// Выполните следующие 10 запросов LINQ to Entities:
//  
// // 1) Получить количество книг определенного жанра.
// // 2) Получить минимальную цену для книг определенного автора.
// // 3) Получить среднюю цену книг в определенном жанре.
// // 4) Получить суммарную стоимость всех книг определенного автора.
// // 5) Выполнить группировку книг по жанрам.
// // 6) Выбрать только названия книг определенного жанра.
// // 7) Выбрать все книги, кроме тех, что относятся к определенному жанру, используя метод Except.
// // 8) Объединить книги от двух авторов, используя метод Union.	
// // 9) Достать 5-ть самых дорогих книг.
// // 10) Пропустить первые 10 книг и взять следующие 5.

using System.Runtime.InteropServices.ComTypes;
using System.Runtime.InteropServices.Marshalling;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            if (!db.Authors.Any() && !db.Genres.Any() && !db.Books.Any())
            {
                var authors = new[]
                {
                    new Author { Name = "Фёдор Достоевский" },
                    new Author { Name = "Лев Толстой" },
                    new Author { Name = "Антон Чехов" },
                    new Author { Name = "Александр Пушкин" },
                    new Author { Name = "Иван Тургенев" }
                };

                var genres = new[]
                {
                    new Genre { Name = "Роман" },
                    new Genre { Name = "Повесть" },
                    new Genre { Name = "Драма" },
                    new Genre { Name = "Поэзия" },
                    new Genre { Name = "Фантастика" }
                };

                var books = new[]
                {
                    new Book { Title = "Преступление и наказание", Author = authors[0], Genre = genres[0], Price = 500 },
                    new Book { Title = "Анна Каренина", Author = authors[1], Genre = genres[0], Price = 600 },
                    new Book { Title = "Вишнёвый сад", Author = authors[2], Genre = genres[2], Price = 300 },
                    new Book { Title = "Евгений Онегин", Author = authors[3], Genre = genres[3], Price = 350 },
                    new Book { Title = "Отцы и дети", Author = authors[3], Genre = genres[1], Price = 450 }
                };


                db.Authors.AddRange(authors);
                db.Genres.AddRange(genres);
                db.Books.AddRange(books);

                db.SaveChanges();
            }
        }

        using (ApplicationContext db = new ApplicationContext())
        {
            // 1) Получить количество книг определенного жанра.
            int booksFantasy = db.Books.Where(b => b.Genre.Name == "Драма").Count();
            
            // 2) Получить минимальную цену для книг определенного автора.
            decimal minPrice = db.Books.Where(b => b.Author.Name == "Лев Толстой").Min(b => b.Price);
            
            // 3) Получить среднюю цену книг в определенном жанре.
            decimal averagePrice = db.Books.Where(b => b.Genre.Name == "Роман").Average(b => b.Price);
        }
    }
}

public class ApplicationContext : DbContext
{
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost;Database=CW;User=sa;Password=admin@Admin87457;TrustServerCertificate=True;");
    }
}

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
    public decimal Price { get; set; }
    public Author Author { get; set; }
    public Genre Genre { get; set; }
}

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Book> Books { get; set; }
}

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Book> Books { get; set; }
}

// using System.Diagnostics;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Diagnostics;
//
// namespace lesson_09_06;
//
// class Program
// {
//     static void Main()
//     {
//         // using (ApplicationContext db = new ApplicationContext())
//         // {
//         //     db.Database.EnsureDeleted();
//         //     db.Database.EnsureCreated();
//         //
//         //     List<Country> countries = new List<Country>
//         //     {
//         //         new Country { Name = "United States" },
//         //         new Country { Name = "Canada" },
//         //         new Country { Name = "United Kingdom" },
//         //         new Country { Name = "Australia" }
//         //     };
//         //
//         //     List<Company> companies = new List<Company>
//         //     {
//         //         new Company { Name = "Acme Corporation", Address = "123 Main St, Cityville, USA", RegistrationDate = new DateTime(1999, 10, 12), Country = countries[0] },
//         //         new Company { Name = "Tech Innovators Ltd.", Address = "456 Tech Blvd, Technocity, Canada", RegistrationDate = new DateTime(2005, 07, 19), Country = countries[1] },
//         //         new Company { Name = "Global Logistics Solutions", Address = "789 Logistics Ave, Transportville, USA", RegistrationDate = new DateTime(1987, 05, 24), Country = countries[0] },
//         //         new Company { Name = "Green Energy Solutions Inc.", Address = "101 Eco Street, Greenburg, United Kingdom", RegistrationDate = new DateTime(2003, 10, 14), Country = countries[2] },
//         //         new Company { Name = "Financial Wizards LLC", Address = "202 Money Lane, Cashville, Australia", RegistrationDate = new DateTime(1994, 08, 22), Country = countries[3] }
//         //     };
//         //
//         //     List<Role> roles = new List<Role>()
//         //     {
//         //         new Role { Name = "Administrator" },
//         //         new Role { Name = "Editor" },
//         //         new Role { Name = "Guest" }
//         //     };
//         //
//         //     List<User> users = new List<User>
//         //     {
//         //         new User { FirstName = "John", LastName = "Doe", PhoneNumber = "+44-204-577-0077", Passport = "AB569784", Age = 21, Salary = 3500, Company = companies[0], Role = roles[0] },
//         //         new User { FirstName = "Jane", LastName = "Smith", PhoneNumber = "+1-800-925-6278", Passport = null, Age = 30, Salary = 2600, Company = companies[2], Role = roles[1] },
//         //         new User { FirstName = "Bob", LastName = "Johnson", PhoneNumber = "+1-800-689-1234", Passport = "CD569784", Age = 40, Salary = 8000, Company = companies[3], Role = roles[2] },
//         //         new User { FirstName = "Alice", LastName = "Williams", PhoneNumber = "+44-111-899-3127", Passport = "EF569784", Age = 19, Salary = 6500, Company = companies[3], Role = roles[0] },
//         //         new User { FirstName = "Charlie", LastName = "Brown", PhoneNumber = "+1-800-976-8901", Passport = "GH569784", Age = 26, Salary = 1800, Company = companies[0], Role = roles[2] },
//         //         new User { FirstName = "Emily", LastName = "Davis", PhoneNumber = "+1-800-632-4567", Passport = "IJ569784", Age = 34, Salary = 2000, Company = companies[1], Role = roles[2] },
//         //         new User { FirstName = "David", LastName = "Miller", PhoneNumber = "+1-800-519-2345", Passport = "KL569784", Age = 16, Salary = 10_000, Company = companies[3], Role = roles[1] },
//         //         new User { FirstName = "Sophia", LastName = "Wilson", PhoneNumber = "+1-800-768-9012", Passport = "MN569784", Age = 22, Salary = 6300, Company = companies[2], Role = roles[0] },
//         //         new User { FirstName = "Ethan", LastName = "Taylor", PhoneNumber = "+44-204-577-0077", Passport = null, Age = 27, Salary = 3800, Company = companies[1], Role = roles[1] },
//         //         new User { FirstName = "Olivia", LastName = "Martin", PhoneNumber = null, Passport = "UV569784", Age = 53, Salary = 2300, Company = companies[2], Role = roles[0] },
//         //         new User { FirstName = "Tom", LastName = "Berel", PhoneNumber = "+1-800-465-1234", Passport = "ST569784", Age = 45, Salary = 2900, Company = companies[2], Role = roles[2] },
//         //         new User { FirstName = "Kate", LastName = "Filips", PhoneNumber = "+1-800-543-6789", Passport = "WX569784", Age = 62, Salary = 4950, Company = companies[0], Role = roles[1] },
//         //         new User { FirstName = "Tom", LastName = "Smith", PhoneNumber = "+44-587-365-4488", Passport = "AA569784", Age = 39, Salary = 1900, Company = companies[0], Role = roles[0] },
//         //         new User { FirstName = "Alan", LastName = "Dilan", PhoneNumber = null, Passport = "YZ569784", Age = 44, Salary = 2450, Company = companies[4], Role = roles[1] },
//         //      };
//         //
//         //     db.Countries.AddRange(countries);
//         //     db.Companies.AddRange(companies);
//         //     db.Roles.AddRange(roles);
//         //     db.Users.AddRange(users);
//         //     db.SaveChanges();
//         // }
//         using (ApplicationContext db = new ApplicationContext())
//         {
//             // var users = db.Users.Where(p => p.Company!.Name == "Google" && p.IsPermanent == true).ToList();
//             // var users = db.Users.Where(p => EF.Functions.Like(p.FirstName!, "%Tom%")).ToList();
//
//             // найти по id
//             // var user = db.Users.Find(1);
//
//             // var user = db.Users.First(e => e.Id == 1);
//             // var user = db.Users.FirstOrDefault(e => e.Id == 1);
//             // var user = db.Users.Last(e => e.Id == 1);
//             // var user = db.Users.LastOrDefault(e => e.Id == 1);
//
//             // var users = db.Users.Select(e => new User
//             // {
//             //     FirstName = e.FirstName,
//             //     Age = e.Age,
//             //     Company = new Company
//             //     {
//             //         Name = e.Company!.Name,
//             //         Country = e.Company.Country
//             //     }
//             // }).ToList();
//
//             // var users = db.Users.OrderByDescending(e => e.Salary).ToList();
//             // var users = db.Users.OrderBy(e => e.Salary).ToList();
//             // var users = db.Users.OrderBy(e => e.Salary).ThenBy(e => e.Age).ToList();
//
//             // var users = db.Users.Join(db.Companies, e => e.CompanyId, c => c.Id, (e,c) => new
//             // {
//             //     Name = e.FirstName,
//             //     CompanyName = c.Name
//             // }).ToList();
//
//             // var users = db.Users.Select(e => new
//             // {
//             //     Name = e.FirstName,
//             //     CompanyName = e.Company!.Name
//             // });
//
//             // var users = db.Users.GroupBy(e => e.Company.Name).Select(e => new
//             // {
//             //     e.Key,
//             //     Count = e.Count()
//             // }).ToList();
//
//             // var result = db.Users.GroupBy(e => e.Company.Country.Name).Select(e => new
//             // {
//             //     CountryName = e.Key,
//             //     UsersCount = e.Count()
//             // });
//
//             // ConCat - обьеденяет с исключениями
//             // var result = db.Countries.Select(e => new { Name = e.Name }).Concat(db.Companies.Select(c => new { Name = c.Name })).ToList();
//
//             // var users = db.Users.Where(u => u.Age > 30).Intersect(db.Users.Where(u => u.FirstName!.Contains("Topm")))
//                 // .ToList();
//             
//             
//         }
//     }
// }
//
// public class Country
// {
//     public int Id { get; set; }
//     public string? Name { get; set; }
//
//     public List<Company> Companies { get; set; }
// }
//
// public class Company
// {
//     public int Id { get; set; }
//     public string? Name { get; set; }
//     public string? Address { get; set; }
//     public DateTime RegistrationDate { get; set; }
//
//     public int CountryId { get; set; }
//     public Country? Country { get; set; }
//     public List<User> Users { get; set; }
// }
//
// public class Role
// {
//     public int Id { get; set; }
//     public string? Name { get; set; }
// }
//
// public class User
// {
//     public int Id { get; set; }
//     public string? FirstName { get; set; }
//     public string? LastName { get; set; }
//     public string? PhoneNumber { get; set; }
//     public string? Passport { get; set; }
//     public int Age { get; set; }
//     public bool IsPermanent { get; set; } = false;
//     public decimal Salary { get; set; }
//
//     public int CompanyId { get; set; }
//     public Company? Company { get; set; }
//
//     public int RoleId { get; set; }
//     public Role? Role { get; set; }
// }
//
// public class ApplicationContext : DbContext
// {
//     public DbSet<User> Users { get; set; } = null!;
//     public DbSet<Role> Roles { get; set; } = null!;
//     public DbSet<Country> Countries { get; set; } = null!;
//     public DbSet<Company> Companies { get; set; } = null!;
//
//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
//         optionsBuilder.LogTo(e => Debug.WriteLine(e), new[] { RelationalEventId.CommandExecuted });
//         optionsBuilder.UseSqlServer(
//             "Server=localhost;Database=CW;User=sa;Password=admin@Admin87457;TrustServerCertificate=True;");
//     }
// }