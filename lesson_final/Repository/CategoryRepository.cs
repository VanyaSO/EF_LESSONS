using lesson_final.Data;
using lesson_final.Interfaces;
using lesson_final.Models;
using lesson_final.Types;
using Microsoft.EntityFrameworkCore;

namespace lesson_final.Repository;

public class CategoryRepository : ICategory
{
    public List<Category> GetAllCategory()
    {
        using (ApplicationContext db = Program.DbContext())
        {
            return db.Categories
                .Include(e => e.Transactions)
                .ToList();
        }
    }
    
    public List<Category> GetAllCategoryByType(OperationType type)
    {
        using (ApplicationContext db = Program.DbContext())
        {
            return db.Categories
                .Where(e => e.Type == type)
                .ToList();
        }
    }

    public Category GetCategory(int id)
    {
        using (ApplicationContext db = Program.DbContext())
        {
            return db.Categories
                .Include(e => e.Transactions)
                .FirstOrDefault(e => e.Id == id);
        }
    }

    public void AddCategory(Category category)
    {
        using (ApplicationContext db = Program.DbContext())
        {
            db.Categories.Add(category);
            db.SaveChanges();
        }
    }

    public void RemoveCategory(Category category)
    {
        using (ApplicationContext db = Program.DbContext())
        {
            db.Categories.Remove(category);
            db.SaveChanges();
        }
    }

    public void UpdateCategory(Category category)
    {
        using (ApplicationContext db = Program.DbContext())
        {
            db.Categories.Update(category);
            db.SaveChanges();
        }
    }
}