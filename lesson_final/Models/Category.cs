using lesson_final.Models;
using lesson_final.Types;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public OperationType Type { get; set; }
    public List<Transaction> Transactions;
}