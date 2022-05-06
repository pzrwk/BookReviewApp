using AutoMapper;
using BookReviewApp.Dto;
using BookReviewApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApp.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthorController : Controller
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    public AuthorController(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
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

    [HttpGet("author/{authorId}")]
    public IActionResult GetAuthorById(int authorId)
    {
        if(!_authorRepository.AuthorExists(authorId))
        {
            return NotFound(new { message = "Author with this id doesn't exist" });
        }

        var author = _mapper.Map<AuthorDto>(_authorRepository.GetAuthor(authorId));

        return Ok(author);
    }

    [HttpGet("book/{authorId}")]
    public IActionResult GetBooksByAuthorId(int authorId)
    {
        var books = _mapper.Map<List<BookDto>>(_authorRepository.GetBooksOfAnAuthor(authorId));

        if(books.Count < 1)
        {
            return NotFound(new { message = "Author with this id doesn't have any books" });
        }

        return Ok(books);
    }
}
