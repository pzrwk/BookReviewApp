using BookReviewApp.Models;

namespace BookReviewApp.Interfaces;

public interface IReviewRepository
{
    ICollection<Review> GetReviews();
    Review GetReview(int id);
    ICollection<Review> GetReviewsOfABook(int bookId);
    bool ReviewExists(int id);
    ICollection<Review> GetReviewsByReviewer(int reviewerId);
    bool CreateReview(Review review);
    bool UpdateReview(Review review);
    bool Save();
}
