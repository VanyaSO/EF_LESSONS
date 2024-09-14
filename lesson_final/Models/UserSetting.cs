using lesson_final.Interfaces;

namespace lesson_final.Models;

public class UserSetting
{
    public int Id { get; set; }
    public string Fullname { get; set; }
    public int Age { get; set; }
    public User User { get; set; }
    public double Balance { get; set; }
}