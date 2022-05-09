using AutoMapper;
using BookReviewApp.Dto;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApp.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthorController : Controller
{
    private readonly IAuthorRepository _authorRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;
    public AuthorController(IAuthorRepository authorRepository, IMapper mapper, ICountryRepository countryRepository)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
        _countryRepository = countryRepository;
    }

    [HttpGet]
    public IActionResult GetAuthors()
    {
        var authors = _mapper.Map<List<AuthorDto>>(_authorRepository.GetAuthors());
        if(authors.Count < 1)
        {
            return NotFound(new {message = "There are no authors in the DB"});
        }

        return Ok(authors);
    }

    [HttpGet("{authorId}")]
    public IActionResult GetAuthorById(int authorId)
    {
        if(!_authorRepository.AuthorExists(authorId))
        {
            return NotFound(new { message = "Author with this id doesn't exist" });
        }

        var author = _mapper.Map<AuthorDto>(_authorRepository.GetAuthor(authorId));

        return Ok(author);
    }

    [HttpGet("{authorId}/books")]
    public IActionResult GetBooksByAuthorId(int authorId)
    {
        var books = _mapper.Map<List<BookDto>>(_authorRepository.GetBooksOfAnAuthor(authorId));

        if(books.Count < 1)
        {
            return NotFound(new { message = "Author with this id doesn't have any books" });
        }

        return Ok(books);
    }

    [HttpPost]
    public IActionResult CreateAuthor([FromQuery] int countryId, [FromBody] AuthorDto authorToBeCreated)
    {
        if (authorToBeCreated == null)
        {
            return BadRequest(new { message = "Invalid author" });
        }

        var author = _authorRepository.GetAuthors()
            .Where(a => a.FirstName.ToLower() == authorToBeCreated.FirstName.ToLower() &&
                            a.LastName.ToLower() == authorToBeCreated.LastName.ToLower())
            .FirstOrDefault();

        if (author != null)
        {
            return Conflict(new { message = "Author already exists" });
        }

        var authorMap = _mapper.Map<Author>(authorToBeCreated);
        var country = _countryRepository.GetCountry(countryId);

        if(country == null)
        {
            return StatusCode(404, new { message = "Country with this id doesn't exist" });
        }

        authorMap.Country = _countryRepository.GetCountry(countryId);

        if (!_authorRepository.CreateAuthor(authorMap))
        {
            return StatusCode(500, new { message = "Something went wrong while saving" });
        }

        return Ok("Successfully created");
    }

    [HttpPut("update/{authorId}")]
    public IActionResult UpdateAuthor(int authorId, [FromBody] AuthorDto updatedAuthor)
    {
        if (updatedAuthor == null)
        {
            return BadRequest(new { message = "Invalid author" });
        }

        if (updatedAuthor.Id != authorId)
        {
            return BadRequest(new { message = "Ids don't match" });
        }

        if (!_authorRepository.AuthorExists(authorId))
        {
            return NotFound(new { message = "Author with this Id doesn't exist" });
        }

        var authorMap = _mapper.Map<Author>(updatedAuthor);

        if (!_authorRepository.UpdateAuthor(authorMap))
        {
            return StatusCode(500, new { message = "Something went wrong while updating author" });
        }

        return Ok("Successfully updated");
    }

    [HttpDelete("{authorId}")]
    public IActionResult DeleteAuthor(int authorId)
    {
        if (!_authorRepository.AuthorExists(authorId))
        {
            return NotFound(new { message = "Author with this Id doesn't exist" });
        }

        var authorToDelete = _authorRepository.GetAuthor(authorId);

        if (!_authorRepository.DeleteAuthor(authorToDelete))
        {
            return StatusCode(500, new { message = "Something went wrong while deleting author" });
        }

        return Ok("Successfully deleted");
    }
}
