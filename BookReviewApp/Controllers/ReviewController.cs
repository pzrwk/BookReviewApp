using AutoMapper;
using BookReviewApp.Dto;
using BookReviewApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApp.Controllers;
[Route("/api/[controller]")]
[ApiController]
public class ReviewController : Controller
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;
    public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
    {
        _reviewRepository = reviewRepository;
        _mapper = mapper;
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
}
