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

    public bool CreateBook(int authorId, int categoryId, Book book)
    {
        var bookAuthorData = _context.Authors.Where(a => a.Id == authorId).FirstOrDefault();
        var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

        var bookAuthor = new BookAuthor()
        {
            Author = bookAuthorData,
            Book = book
        };

        _context.Add(bookAuthor);

        var bookCategory = new BookCategory()
        {
            Category = category,
            Book = book
        };

        _context.Add(bookCategory);

        _context.Add(book);

        return Save();
    }

    public bool Save()
    {
        var save = _context.SaveChanges();

        return save > 0 ? true : false;
    }

    public bool UpdateBook(int authorId, int categoryId, Book book)
    {
        _context.Update(book);
        return Save();
    }
}
