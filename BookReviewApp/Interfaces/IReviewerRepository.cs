using BookReviewApp.Models;

namespace BookReviewApp.Interfaces;

public interface IReviewerRepository
{
    Reviewer GetReviewer(int id);
    ICollection<Reviewer> GetReviewers();
    bool ReviewerExists(int id);
    bool CreateReviewer(Reviewer reviewer);
    bool UpdateReviewer(Reviewer reviewer);
    bool Save();
}
