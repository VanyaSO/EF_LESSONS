using lesson_final.Types;

namespace lesson_final.Interfaces;

public interface ICategory
{
    List<Category> GetAllCategoryByType(OperationType type);
}