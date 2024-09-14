using lesson_09_09_2.Data;
using lesson_09_09_2.Interfaces;
using lesson_09_09_2.Models;
using lesson_09_09_2.Repository;
using Microsoft.EntityFrameworkCore;

namespace lesson_09_09_2;

class Program
{
    public static ApplicationContext DbContext() => new ApplicationContextFactory().CreateDbContext();
    private static IBook _books;
    
    enum ShopMenu
    {
        Books, Authors, Categories, Orders, SearchAuthors, SearchBooks, SearchCategories, SearchOrders, AddBook, AddAuthor, AddCategory, AddOrder, Exit
    }
    
    static async Task Main()
    {
        Initialize();
        int input = ConsoleHelper.MultipleChoice(true, new ShopMenu());
        Console.WriteLine(input);
        
        // var allBooks = await _books.GetBooksWithAuthorsAndReviewAndCategoryAsync(5);
    }

    static void Initialize()
    {
        new DbInit().Init(DbContext());
        _books = new BookRepository();
    }
};
