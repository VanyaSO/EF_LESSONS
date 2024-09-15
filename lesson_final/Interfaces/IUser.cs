using lesson_final.Models;
namespace lesson_final.Interfaces;


public interface IUser
{
    User GetUserByEmail(string email);
    void UpdateUser(User user);
}