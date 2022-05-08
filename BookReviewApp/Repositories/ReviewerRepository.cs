using BookReviewApp.Data;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;

namespace BookReviewApp.Repositories;

public class ReviewerRepository : IReviewerRepository
{
    public readonly DataContext _context;
    public ReviewerRepository(DataContext context)
    {
        _context = context;
    }

    public bool CreateReviewer(Reviewer reviewer)
    {
        _context.Add(reviewer);
        return Save();
    }

    public Reviewer GetReviewer(int id)
    {
        return _context.Reviewers.Where(x => x.Id == id).FirstOrDefault();
    }

    public ICollection<Reviewer> GetReviewers()
    {
        return _context.Reviewers.OrderBy(x => x.Id).ToList();
    }

    public bool ReviewerExists(int id)
    {
        return _context.Reviewers.Any(x => x.Id == id);
    }
    public bool Save()
    {
        var save = _context.SaveChanges();

        return save > 0 ? true : false;
    }

    public bool UpdateReviewer(Reviewer reviewer)
    {
        _context.Update(reviewer);
        return Save();
    }
}
