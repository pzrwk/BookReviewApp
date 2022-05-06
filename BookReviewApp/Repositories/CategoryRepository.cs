using BookReviewApp.Data;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;

namespace BookReviewApp.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DataContext _context;
    public CategoryRepository(DataContext context)
    {
        _context = context;
    }
    public bool CategoryExists(int id)
    {
        return _context.Categories.Any(c => c.Id == id);
    }

    public bool CategoryExists(string name)
    {
        return _context.Categories.Any(c => c.Name.Equals(name));
    }

    public ICollection<Book> GetBooksByCategory(int categoryId)
    {
        return _context.BookCategories.Where(bc => bc.CategoryId == categoryId).Select(c => c.Book).ToList();
    }

    public ICollection<Category> GetCategories()
    {
        return _context.Categories.OrderBy(c => c.Id).ToList();
    }

    public Category GetCategory(int id)
    {
        return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
    }

    public Category GetCategory(string name)
    {
        return _context.Categories.Where(c => c.Name.Equals(name)).FirstOrDefault();
    }
}
