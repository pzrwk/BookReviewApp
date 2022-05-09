using AutoMapper;
using BookReviewApp.Dto;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;
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

    [HttpPost]
    public IActionResult CreateCategory([FromBody] CategoryDto categoryToBeCreated)
    {
        if(categoryToBeCreated == null)
        {
            return BadRequest(new { message = "Invalid category" });
        }

        var category = _categoryRepository.GetCategories()
            .Where(c => c.Name.ToLower() == categoryToBeCreated.Name.ToLower())
            .FirstOrDefault();

        if(category != null)
        {
            return Conflict(new { message = "Category already exists" });
        }

        var categoryMap = _mapper.Map<Category>(categoryToBeCreated);

        if (!_categoryRepository.CreateCategory(categoryMap))
        {
            return StatusCode(500, new { message = "Something went wrong while saving" });
        }

        return Ok("Successfully created");
    }

    [HttpPut("update/{categoryId}")]
    public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto updatedCategory)
    {
        if(updatedCategory == null)
        {
            return BadRequest(new { message = "Invalid category" });
        }

        if(updatedCategory.Id != categoryId)
        {
            return BadRequest(new { message = "Ids don't match" });
        }

        if(!_categoryRepository.CategoryExists(categoryId))
        {
            return NotFound(new { message = "Category with this Id doesn't exist" });
        }

        var categoryMap = _mapper.Map<Category>(updatedCategory);

        if(!_categoryRepository.UpdateCategory(categoryMap))
        {
            return StatusCode(500, new { message = "Something went wrong while updating category" });
        }

        return Ok("Successfully updated");
    }

    [HttpDelete("{categoryId}")]
    public IActionResult DeleteCategory(int categoryId)
    {
        if (!_categoryRepository.CategoryExists(categoryId))
        {
            return NotFound(new { message = "Category with this Id doesn't exist" });
        }

        var categoryToDelete = _categoryRepository.GetCategory(categoryId);

        if (!_categoryRepository.DeleteCategory(categoryToDelete))
        {
            return StatusCode(500, new { message = "Something went wrong while deleting category" });
        }

        return Ok("Successfully deleted");
    }
}
