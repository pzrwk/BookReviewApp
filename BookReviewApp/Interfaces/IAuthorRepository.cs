using BookReviewApp.Models;

namespace BookReviewApp.Interfaces;

public interface IAuthorRepository
{
    ICollection<Author> GetAuthors();
    Author GetAuthor(int id);
    bool AuthorExists(int id);
    ICollection<Book> GetBooksOfAnAuthor(int authorId);
}
