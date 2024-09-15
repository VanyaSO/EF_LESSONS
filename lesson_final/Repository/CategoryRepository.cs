using lesson_final.Data;
using lesson_final.Interfaces;
using lesson_final.Models;
using lesson_final.Types;
using Microsoft.EntityFrameworkCore;

namespace lesson_final.Repository;

public class CategoryRepository : ICategory
{
    public List<Category> GetAllCategoryByType(OperationType type)
    {
        using (ApplicationContext db = Program.DbContext())
        {
            return db.Categories
                .Where(e => e.Type == type)
                .ToList();
        }
    }
}