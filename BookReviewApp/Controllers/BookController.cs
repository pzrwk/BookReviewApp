using AutoMapper;
using BookReviewApp.Dto;
using BookReviewApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : Controller
{
    private readonly IBookRepository _bookRepository;
    private readonly IMapper _mapper;

    public BookController(IBookRepository bookRepository, IMapper mapper)
    {
        _bookRepository = bookRepository;
        _mapper = mapper;
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
}
