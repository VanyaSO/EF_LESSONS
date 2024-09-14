using lesson_final.Interfaces;

namespace lesson_final.ViewModels;

public class ItemView : IShow<int>
{
    public int Id { get; set; }
    public string Value { get; set; }
}