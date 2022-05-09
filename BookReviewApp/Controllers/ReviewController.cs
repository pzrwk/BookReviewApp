using AutoMapper;
using BookReviewApp.Dto;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApp.Controllers;
[Route("/api/[controller]")]
[ApiController]
public class ReviewController : Controller
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IReviewerRepository _reviewerRepository;
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;
    public ReviewController(IReviewRepository reviewRepository, IMapper mapper, IReviewerRepository reviewerRepository, IBookRepository bookRepository)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
        _reviewerRepository = reviewerRepository;
        _bookRepository = bookRepository;
    }

    [HttpGet]
    public IActionResult GetReviews()
    {
        var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviews());

        if(reviews.Count < 1)
        {
            return NotFound(new { message = "There are no reviews in the DB" });
        }

        return Ok(reviews);
    }

    [HttpGet("{reviewId}")]
    public IActionResult GetReview(int reviewId)
    {
        if (!_reviewRepository.ReviewExists(reviewId))
        {
            return NotFound(new { message = "Review with this id doesn't exist" });
        }

        var review = _mapper.Map<ReviewDto>(_reviewRepository.GetReview(reviewId));

        return Ok(review);
    }

    [HttpGet("book/{bookId}")]
    public IActionResult GetReviewsOfABook(int bookId)
    {
        var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsOfABook(bookId));

        if(reviews.Count < 1)
        {
            return NotFound(new { message = "This books has no reviews" });
        }

        return Ok(reviews);
    }

    [HttpGet("reviewer/{reviewerId}")]
    public IActionResult GetReviewsOfAReviewer(int reviewerId)
    {
        var reviews = _mapper.Map<List<ReviewDto>>(_reviewRepository.GetReviewsByReviewer(reviewerId));
        if (reviews.Count < 1)
        {
            return NotFound(new { message = "This reviewer didn't review anything" });
        }

        return Ok(reviews);
    }

    [HttpPost]
    public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int bookId, [FromBody] ReviewDto reviewToBeCreated)
    {
        if(reviewToBeCreated == null)
        {
            return BadRequest(new { message = "Invalid review" });
        }

        var reviews = _reviewRepository.GetReviews()
            .Where(r => r.Title.ToLower() == reviewToBeCreated.Title.ToLower())
            .FirstOrDefault();

        if(reviews != null)
        {
            return Conflict(new { message = "Review already exists" });
        }

        var reviewMap = _mapper.Map<Review>(reviewToBeCreated);

        reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);
        reviewMap.Book = _bookRepository.GetBook(bookId);

        if (!_reviewRepository.CreateReview(reviewMap))
        {
            return StatusCode(500, new { message = "Something went wrong while saving" });
        }

        return Ok("Successfully created");
    }

    [HttpPut("update/{reviewId}")]
    public IActionResult UpdateCategory(int reviewId, [FromBody] ReviewDto updatedReview)
    {
        if (updatedReview == null)
        {
            return BadRequest(new { message = "Invalid review" });
        }

        if (updatedReview.Id != reviewId)
        {
            return BadRequest(new { message = "Ids don't match" });
        }

        if (!_reviewRepository.ReviewExists(reviewId))
        {
            return NotFound(new { message = "Review with this Id doesn't exist" });
        }

        var reviewMap = _mapper.Map<Review>(updatedReview);

        if (!_reviewRepository.UpdateReview(reviewMap))
        {
            return StatusCode(500, new { message = "Something went wrong while updating review" });
        }

        return Ok("Successfully updated");
    }

    [HttpDelete("{reviewId}")]
    public IActionResult DeleteReview(int reviewId)
    {
        if (!_reviewRepository.ReviewExists(reviewId))
        {
            return NotFound(new { message = "Review with this Id doesn't exist" });
        }

        var reviewToDelete = _reviewRepository.GetReview(reviewId);

        if (!_reviewRepository.DeleteReview(reviewToDelete))
        {
            return StatusCode(500, new { message = "Something went wrong while deleting review" });
        }

        return Ok("Successfully deleted");
    }
}
