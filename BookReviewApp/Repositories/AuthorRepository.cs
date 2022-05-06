using BookReviewApp.Data;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;

namespace BookReviewApp.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly DataContext _context;
    public AuthorRepository(DataContext context)
    {
        _context = context;
    }
    public bool AuthorExists(int id)
    {
        return _context.Authors.Any(a => a.Id == id);
    }

    public Author GetAuthor(int id)
    {
        return _context.Authors.Where(a => a.Id == id).FirstOrDefault();
    }

    public ICollection<Author> GetAuthors()
    {
        return _context.Authors.OrderBy(a => a.Id).ToList();
    }

    public ICollection<Book> GetBooksOfAnAuthor(int authorId)
    {
        return _context.BookAuthors.Where(bc => bc.AuthorId == authorId).Select(a => a.Book).ToList();
    }
}
