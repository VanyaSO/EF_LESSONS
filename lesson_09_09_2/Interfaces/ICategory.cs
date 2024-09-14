using lesson_09_09_2.Models;

namespace lesson_09_09_2.Interfaces;

public interface ICategory
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();
    Task<IEnumerable<Category>> GetCategoriesByNameAsync(string name);
    Task<Category> GetCategoryAsync(int id);
    Task<Category> GetCategoryWithBooksAsync(int id);
        
    Task AddCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);
}
