using Microsoft.EntityFrameworkCore;

namespace lesson_09_04;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}

public class ApplicationContext : DbContext
{
 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=CH;User=sa;Password=admin@Admin87457; TrustServerCertificate = True;");
    }
}