using BookReviewApp.Data;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;

namespace BookReviewApp.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly DataContext _context;
    public ReviewRepository(DataContext context)
    {
        _context = context;
    }
    public Review GetReview(int id)
    {
        return _context.Reviews.Where(r => r.Id == id).FirstOrDefault();
    }

    public ICollection<Review> GetReviews()
    {
        return _context.Reviews.OrderBy(r => r.Id).ToList();
    }

    public ICollection<Review> GetReviewsByReviewer(int reviewerId)
    {
        return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
    }

    public ICollection<Review> GetReviewsOfABook(int bookId)
    {
        return _context.Reviews.Where(r => r.Book.Id == bookId).ToList();
    }

    public bool ReviewExists(int id)
    {
        return _context.Reviews.Any(r => r.Id == id);
    }
}
