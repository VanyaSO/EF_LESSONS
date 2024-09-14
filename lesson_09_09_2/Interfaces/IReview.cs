using lesson_09_09_2.Models;

namespace lesson_09_09_2.Interfaces;

public interface IReview
{
    Task<IEnumerable<Review>> GetAllReviewsAsync(int bookId);
    Task<Review> GetReviewAsync(int id);

    Task AddReviewAsync(Review review);
    Task DeleteReviewAsync(Review review);
}