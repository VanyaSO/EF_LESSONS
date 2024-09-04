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
            // db.Database.EnsureCreated();

            db.SaveChanges();
        }
    }
}

public class User

{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? Position { get; set; }
    public decimal Salary { get; set; }
}

public class Company
{
    public int Id { get; set; }
    public string? Name { get; set; }
}


public class Calender {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Date { get; set; }
}
public class ApplicationContext : DbContext

{
    // private readonly StreamWriter writer = new StreamWriter("../../../log.txt", true);
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Company> Companies { get; set; } = null!;
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = database.db");
        // optionsBuilder.UseSqlServer("Server=localhost;Database=test;User=sa;Password=admin@Admin87457;TrustServerCertificate=True;");
        // optionsBuilder.LogTo(writer.WriteLine);
        // optionsBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name });

        //RelationalEventId
        //CoreEventId
        //SqlServerEventId
        // optionsBuilder.LogTo(message => writer.WriteLine(message), new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
        //optionsBuilder.LogTo(Console.WriteLine);
        //optionsBuilder.LogTo((message) =>
        //{
        //    //...
        //    Debug.WriteLine(message);
        //});


        // свои логирования 


    }
}