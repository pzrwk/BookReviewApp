using BookReviewApp.Models;

namespace BookReviewApp.Interfaces;

public interface IBookRepository
{
    ICollection<Book> GetBooks();
    Book GetBook(int id);
    Book GetBook(string name);
    decimal GetBookRating(int bookId);
    bool BookExists(int id);
    bool BookExists(string title);
}
