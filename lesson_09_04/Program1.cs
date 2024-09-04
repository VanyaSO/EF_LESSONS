// using Microsoft.EntityFrameworkCore;
//
// namespace lesson_09_04;
//
// class Program1
// {
//     static void Main()
//
//     {
//         // using (ApplicationContext db = new ApplicationContext())
//         // {
//         //     db.Database.EnsureDeleted();
//         //
//         //     db.Database.EnsureCreated();
//         //     Company microsoft = new Company { Name = "Microsoft" };
//         //     Company google = new Company { Name = "Google" };
//         //     db.Companies.AddRange(microsoft, google);
//         //
//         //     User tom = new User { Name = "Tom", Company = microsoft };
//         //     User bob = new User { Name = "Bob", Company = microsoft };
//         //     User alice = new User { Name = "Alice", Company = google };
//         //     db.Users.AddRange(tom, bob, alice);
//         //
//         //     db.SaveChanges();
//         // }
//
//         using (ApplicationContext db = new ApplicationContext())
//         {
//             db.Database.EnsureDeleted();
//             db.Database.EnsureCreated();
//             //
//             // var st1 = new Student { Name = "F" };
//             // var st2 = new Student { Name = "A" };
//             // db.Students.AddRange(st1, st2);
//             //
//             // db.Courses.AddRange(new Course { Name = "C#", Students = new() {st1, st2} }, new Course { Name = "Math" });
//             //
//             // Student? st = db.Students.Include(s => s.Courses).FirstOrDefault(s => s.Id == 1);
//             // Course? math = db.Courses.FirstOrDefault(c => c.Name == "Math");
//             // Course? c = db.Courses.FirstOrDefault(c => c.Name == "C#");
//
//             // st.Courses.Remove(c);
//             // st.Courses.Add(math);
//             // st.Courses.Add(c);
//             
//             
//             db.SaveChanges();
//         }
//     }
//
//     // public class Company
//     // {
//     //     public int Id { get; set; }
//     //     public string? Name { get; set; }
//     //     public List<User> Users { get; set; }
//     // }
//     //
//     // public class User
//     // {
//     //     public int Id { get; set; }
//     //     public string? Name { get; set; }
//     //
//     //     public int CompanyId { get; set; }
//     //     public Company? Company { get; set; }
//     // }
//
//     public class Course
//     {
//         public int Id { get; set; }
//         public string? Name { get; set; }
//         public List<Student> Students { get; set; } = new();
//     }
//
//     public class Student
//     {
//         public int Id { get; set; }
//         public string? Name { get; set; }
//         public List<Course> Courses { get; set; } = new();
//     }
//
//
//     public class ApplicationContext : DbContext
//     {
//         public DbSet<Course> Courses { get; set; }
//         public DbSet<Student> Students { get; set; }
//
//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             optionsBuilder.UseSqlServer(
//                 "Server=localhost;Database=CW;User=sa;Password=admin@Admin87457; TrustServerCertificate = True;");
//         }
//
//         protected override void OnModelCreating(ModelBuilder modelBuilder)
//         {
//             // modelBuilder.Entity<User>()
//             //     .HasOne(e => e.Company)
//             //     .WithMany(e => e.Users)
//             //     .HasForeignKey(e => e.CompanyId);
//
//             modelBuilder.Entity<Course>()
//                 .HasMany(e => e.Students)
//                 .WithMany(e => e.Courses)
//                 .UsingEntity(e => e.ToTable("Enrollments"));
//
//             base.OnModelCreating(modelBuilder);
//         }
//     }
// }