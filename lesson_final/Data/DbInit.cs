using lesson_final.Models;
using lesson_final.Types;

namespace lesson_final.Data;

public class DbInit
{
    public void Init(ApplicationContext db)
    {
        if (!db.Users.Any())
        {
            db.Users.AddRange(new List<User>
            {
                new User
                {
                    Email = "ushachovg324@gmail.com", Password = "12345",
                    UserSetting = new UserSetting { Fullname = "Ivan Ushachov", Age = 20, Balance = 1000 }
                }
            });
            db.SaveChanges();
        }

        if (!db.Categories.Any())
        {
            db.Categories.AddRange(new List<Category>
            {
                new Category { Name = "Products", Type = OperationType.Consumption },
                new Category { Name = "Books", Type = OperationType.Consumption },
                new Category() { Name = "Receipts", Type = OperationType.Income }
            });
            db.SaveChanges();
        }

        if (!db.Transactions.Any())
        {
            db.Transactions.AddRange(new List<Transaction>
            {
                new Transaction { Type = OperationType.Income, Sum = 200, UserId = db.Users.FirstOrDefault(e => e.Email == "ushachovg324@gmail.com").Id, CategoryId = db.Categories.FirstOrDefault(e => e.Name.Contains("Receipts")).Id},
                new Transaction
                {
                    Type = OperationType.Consumption, Sum = 700, UserId = db.Users.FirstOrDefault(e => e.Email == "ushachovg324@gmail.com").Id,
                    CategoryId = db.Categories.FirstOrDefault(e => e.Name.Contains("Products")).Id
                }
            });
            db.SaveChanges();
        }
    }
}