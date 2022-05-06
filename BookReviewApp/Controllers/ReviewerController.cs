using AutoMapper;
using BookReviewApp.Dto;
using BookReviewApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApp.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class ReviewerController : Controller
{
    private readonly IReviewerRepository _reviewerRepository;
    private readonly IMapper _mapper;
    public ReviewerController(IReviewerRepository reviewerRepository, IMapper mapper)
    {
        _reviewerRepository = reviewerRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetReviewers()
    {
        var reviewers = _mapper.Map<List<ReviewerDto>>(_reviewerRepository.GetReviewers());

        if(reviewers.Count < 1)
        {
            return NotFound(new { message = "There are no reviewers in the DB" });
        }

        return Ok(reviewers);
    }

    [HttpGet("{reviewerId}")]
    public IActionResult GetReviewer(int reviewerId)
    {
        if(!_reviewerRepository.ReviewerExists(reviewerId))
        {
            return NotFound(new { message = "Reviewer with this id doesn't exist" });
        }

        var reviewer = _mapper.Map<ReviewerDto>(_reviewerRepository.GetReviewer(reviewerId));

        return Ok(reviewer);
    }
}
