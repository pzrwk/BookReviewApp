using BookReviewApp.Data;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;

namespace BookReviewApp.Repositories;

public class BookRepository : IBookRepository
{
    private readonly DataContext _context;

    public BookRepository(DataContext context)
    {
        _context = context;
    }
    public ICollection<Book> GetBooks()
    {
        return _context.Books.OrderBy(b => b.Id).ToList();
    }
    public Book GetBook(int id)
    {
        return _context.Books.Where(b => b.Id == id).FirstOrDefault();
    }

    public Book GetBook(string title)
    {
        return _context.Books.Where(b => b.Title.Equals(title)).FirstOrDefault();
    }

    public decimal GetBookRating(int bookId)
    {
        var review = _context.Reviews.Where(r => r.Book.Id == bookId);

        if (review.Count() < 1)
        {
            return 0;
        }

        return (decimal)review.Sum(r => r.Rating) / review.Count();
    }

    public bool BookExists(int id)
    {
        return _context.Books.Any(b => b.Id == id);
    }
    public bool BookExists(string title)
    {
        return _context.Books.Any(b => b.Title.Equals(title));
    }
}
