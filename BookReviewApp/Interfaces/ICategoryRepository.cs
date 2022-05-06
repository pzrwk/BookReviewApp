using BookReviewApp.Models;

namespace BookReviewApp.Interfaces;

public interface ICategoryRepository
{
    ICollection<Category> GetCategories();
    Category GetCategory(int id);
    Category GetCategory(string name);
    ICollection<Book> GetBooksByCategory(int categoryId);
    bool CategoryExists(int id);
    bool CategoryExists(string name);
}
