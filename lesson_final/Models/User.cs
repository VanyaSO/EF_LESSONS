namespace lesson_final.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int UserSettingId { get; set; }
    public UserSetting UserSetting { get; set; }
    public List<Transaction> Transactions { get; set; }
}