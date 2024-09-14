using lesson_final.Types;

namespace lesson_final.Models;

public class Transaction
{
    public int Id { get; set; }
    public OperationType Type { get; set; }
    public double Sum { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Description { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public override string ToString()
    {
        string result = $"Type: {Type}, Sum: {Sum}, Category: {Category.Name}";
        if (!string.IsNullOrEmpty(Description))
        {
            result += $", Description: {Description}";
        }
        return result;
    }
}