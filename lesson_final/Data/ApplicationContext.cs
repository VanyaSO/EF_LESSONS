using lesson_final.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace lesson_final.Data;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Category> Categories { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) 
        : base(options)
    {
    }
    


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(e => e.UserSetting)
            .WithOne(e => e.User)
            .HasForeignKey<User>(e => e.UserSettingId);
        
        modelBuilder.Entity<Transaction>()
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql("GETDATE()");
    }
    
}