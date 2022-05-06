using BookReviewApp.Models;

namespace BookReviewApp.Interfaces;

public interface IReviewRepository
{
    ICollection<Review> GetReviews();
    Review GetReview(int id);
    ICollection<Review> GetReviewsOfABook(int bookId);
    bool ReviewExists(int id);
    ICollection<Review> GetReviewsByReviewer(int reviewerId);
}
