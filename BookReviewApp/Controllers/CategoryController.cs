using AutoMapper;
using BookReviewApp.Dto;
using BookReviewApp.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : Controller
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());

        if(categories.Count < 1)
        {
            return NotFound(new { message = "There are no categories in the DB." });
        }

        return Ok(categories);
    }

    [HttpGet("id/{categoryId}")]
    public IActionResult GetCategoryById(int categoryId)
    {
        if(!_categoryRepository.CategoryExists(categoryId))
        {
            return NotFound(new { message = "Category with this id doesn't exist" });
        }

        var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(categoryId));

        return Ok(category);
    }

    [HttpGet("name/{categoryName}")]
    public IActionResult GetCategoryByName(string categoryName)
    {
        if (!_categoryRepository.CategoryExists(categoryName))
        {
            return NotFound(new { message = "Category with this name doesn't exist" });
        }

        var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(categoryName));

        return Ok(category);
    }

    [HttpGet("book/{categoryId}")]
    public IActionResult GetBookByCategoryId(int categoryId)
    {
        var books = _mapper.Map<List<BookDto>>(_categoryRepository.GetBooksByCategory(categoryId));
        
        if(books.Count < 1)
        {
            return NotFound(new { message = "There are no books under this category" });
        }

        return Ok(books);
    }
}
