using lesson_final.Data;
using lesson_final.Interfaces;
using lesson_final.Models;
using Microsoft.EntityFrameworkCore;

namespace lesson_final.Repository;

public class UserRepository : IUser
{
    public User GetUserByEmail(string email)
    {
        using (ApplicationContext db = Program.DbContext())
        {
            return db.Users
                .Include(e => e.UserSetting)
                .Include(e => e.Transactions).ThenInclude(t => t.Category)
                .FirstOrDefault(e => e.Email == email);
        }
    }

    public void UpdateUser(User user)
    {
        using (ApplicationContext db = Program.DbContext())
        {
            db.Users.Update(user);
            db.SaveChanges();
        }
    }
}