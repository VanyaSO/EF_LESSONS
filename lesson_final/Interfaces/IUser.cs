using lesson_final.Models;
namespace lesson_final.Interfaces;


public interface IUser
{
    User GetUserByEmail(string email);

    void AddUser(User user);
    void RemoveUser(User user);
    void UpdateUser(User user);
}