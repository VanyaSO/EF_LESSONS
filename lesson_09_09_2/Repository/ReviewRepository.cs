using lesson_09_09_2.Data;
using lesson_09_09_2.Interfaces;
using lesson_09_09_2.Models;
using Microsoft.EntityFrameworkCore;

namespace lesson_09_09_2.Repository;

public class ReviewRepository : IReview
{
    public async Task AddReviewAsync(Review review)
    {
        using (ApplicationContext context = Program.DbContext())
        {
            await context.Reviews.AddAsync(review);
            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteReviewAsync(Review review)
    {
        using (ApplicationContext context = Program.DbContext())
        {
            context.Reviews.Remove(review);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Review>> GetAllReviewsAsync(int bookId)
    {
        using (ApplicationContext context = Program.DbContext())
        {
            return await context.Reviews.Where(e => e.BookId == bookId).ToListAsync();
        }
    }

    public async Task<Review> GetReviewAsync(int id)
    {
        using (ApplicationContext context = Program.DbContext())
        {
            return await context.Reviews.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}