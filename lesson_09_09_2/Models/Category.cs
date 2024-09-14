namespace lesson_09_09_2.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ICollection<Book> Books { get; set; }
}
