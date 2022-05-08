using AutoMapper;
using BookReviewApp.Dto;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : Controller
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public BookController(IBookRepository bookRepository, IMapper mapper, IAuthorRepository authorRepository, ICategoryRepository categoryRepository)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
        _authorRepository = authorRepository;
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public IActionResult GetBooks()
    {
        var books = _mapper.Map<List<BookDto>>(_bookRepository.GetBooks());

        if(books.Count < 1)
        {
            return NotFound(new { message = "There are no books in DB." });
        }
        return Ok(books);
    }

    [HttpGet("id/{bookId}")]
    public IActionResult GetBookById(int bookId)
    {
        if (!_bookRepository.BookExists(bookId))
        {
            return NotFound(new { message = "Book with this id doesn't exist" });
        }

        var book = _mapper.Map<BookDto>(_bookRepository.GetBook(bookId));

        return Ok(book);
    }

    [HttpGet("title/{bookTitle}")]
    public IActionResult GetBookByTitle(string bookTitle)
    {
        if(!_bookRepository.BookExists(bookTitle))
        {
            return NotFound(new {message = "Book with this title doesn't exist" });
        }

        var book = _mapper.Map<BookDto>(_bookRepository.GetBook(bookTitle));

        return Ok(book);
    }

    [HttpGet("{bookId}/rating")]
    public IActionResult GetBookRating(int bookId)
    {
        if (!_bookRepository.BookExists(bookId))
        {
            return NotFound(new { message = "Book with this id doesn't exist" });
        }

        var rating = _bookRepository.GetBookRating(bookId);

        return Ok(rating);
    }

    [HttpPost]
    public IActionResult CreateBook([FromQuery] int authorId,[FromQuery] int categoryId, [FromBody] BookDto bookToBeCreated)
    {
        if (bookToBeCreated == null)
        {
            return BadRequest(new { message = "Invalid book" });
        }

        var book = _bookRepository.GetBooks()
            .Where(b => b.Title.ToLower() == bookToBeCreated.Title.ToLower())
            .FirstOrDefault();

        if (book != null)
        {
            return Conflict(new { message = "Book already exists" });
        }

        var bookMap = _mapper.Map<Book>(bookToBeCreated);
        var author = _authorRepository.GetAuthor(authorId);
        var category = _categoryRepository.GetCategory(categoryId);

        if (author == null)
        {
            return StatusCode(404, new { message = "Country with this id doesn't exist" });
        }

        if(category == null)
        {
            return StatusCode(404, new { message = "Category with this id doesn't exist" });
        }

        if (!_bookRepository.CreateBook(authorId, categoryId, bookMap))
        {
            return StatusCode(500, new { message = "Something went wrong while saving" });
        }

        return Ok("Successfully created");
    }

    [HttpPut("update/{bookId}")]
    public IActionResult UpdateCategory(int bookId, [FromQuery] int authorId, [FromQuery] int categoryId, [FromBody] BookDto updatedBook)
    {
        if (updatedBook == null)
        {
            return BadRequest(new { message = "Invalid book" });
        }

        if (updatedBook.Id != bookId)
        {
            return BadRequest(new { message = "Ids don't match" });
        }

        if (!_bookRepository.BookExists(bookId))
        {
            return NotFound(new { message = "Book with this Id doesn't exist" });
        }

        var bookMap = _mapper.Map<Book>(updatedBook);

        if (!_bookRepository.UpdateBook(authorId, categoryId, bookMap))
        {
            return StatusCode(500, new { message = "Something went wrong while updating book" });
        }

        return Ok("Successfully updated");
    }
}
