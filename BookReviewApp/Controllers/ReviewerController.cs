using AutoMapper;
using BookReviewApp.Dto;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;
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

    [HttpPost]
    public IActionResult CreateReviewer([FromBody] ReviewerDto reviewerToBeCreated)
    {
        if (reviewerToBeCreated == null)
        {
            return BadRequest(new { message = "Invalid reviewer" });
        }

        var reviewers = _reviewerRepository.GetReviewers()
            .Where(r => r.FirstName.ToLower() == reviewerToBeCreated.FirstName.ToLower() &&
                            r.LastName.ToLower() == reviewerToBeCreated.LastName.ToLower())
            .FirstOrDefault();

        if (reviewers != null)
        {
            return Conflict(new { message = "Reviewer already exists" });
        }

        var reviewerMap = _mapper.Map<Reviewer>(reviewerToBeCreated);

        if (!_reviewerRepository.CreateReviewer(reviewerMap))
        {
            return StatusCode(500, new { message = "Something went wrong while saving" });
        }

        return Ok("Successfully created");
    }

    [HttpPut("update/{reviewerId}")]
    public IActionResult UpdateCategory(int reviewerId, [FromBody] ReviewerDto updatedReviewer)
    {
        if (updatedReviewer == null)
        {
            return BadRequest(new { message = "Invalid reviewer" });
        }

        if (updatedReviewer.Id != reviewerId)
        {
            return BadRequest(new { message = "Ids don't match" });
        }

        if (!_reviewerRepository.ReviewerExists(reviewerId))
        {
            return NotFound(new { message = "Reviewer with this Id doesn't exist" });
        }

        var reviewerMap = _mapper.Map<Reviewer>(updatedReviewer);

        if (!_reviewerRepository.UpdateReviewer(reviewerMap))
        {
            return StatusCode(500, new { message = "Something went wrong while updating reviewer" });
        }

        return Ok("Successfully updated");
    }

    [HttpDelete("{reviewerId}")]
    public IActionResult DeleteReviewer(int reviewerId)
    {
        if (!_reviewerRepository.ReviewerExists(reviewerId))
        {
            return NotFound(new { message = "Reviewer with this Id doesn't exist" });
        }

        var reviewerToDelete = _reviewerRepository.GetReviewer(reviewerId);

        if (!_reviewerRepository.DeleteReviewer(reviewerToDelete))
        {
            return StatusCode(500, new { message = "Something went wrong while deleting reviewer" });
        }

        return Ok("Successfully deleted");
    }
}
