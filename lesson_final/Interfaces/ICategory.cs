using lesson_final.Types;

namespace lesson_final.Interfaces;

public interface ICategory
{
    List<Category> GetAllCategory();
    List<Category> GetAllCategoryByType(OperationType type);
    Category GetCategory(int id);

    void AddCategory(Category category);
    void RemoveCategory(Category category);
    void UpdateCategory(Category category);
}