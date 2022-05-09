using AutoMapper;
using BookReviewApp.Dto;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : Controller
{
    private readonly ICountryRepository _countryRepository;
    private readonly IMapper _mapper;

    public CountryController(ICountryRepository countryRepository, IMapper mapper)
    {
        _countryRepository = countryRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetCountries()
    {
        var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());

        if(countries.Count < 1)
        {
            return NotFound(new {message = "There are no countries in the DB."});
        }

        return Ok(countries);
    }

    [HttpGet("id/{countryId}")]
    public IActionResult GetCountryById(int countryId)
    {
        if(!_countryRepository.CountryExists(countryId))
        {
            return NotFound(new { message = "Country with this id doesn't exist" });
        }

        var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryId));

        return Ok(country);
    }

    [HttpGet("name/{countryName}")]
    public IActionResult GetCountryByName(string countryName)
    {
        if (!_countryRepository.CountryExists(countryName))
        {
            return NotFound(new { message = "Country with this name doesn't exist" });
        }

        var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryName));

        return Ok(country);
    }

    [HttpGet("author/{authorId}")]
    public IActionResult GetCountryOfAnAuthor(int authorId)
    {
        var country = _mapper.Map<CountryDto>(_countryRepository.GetCountryByAuthor(authorId));

        return Ok(country);
    }

    [HttpGet("{countryId}/authors")]
    public IActionResult GetAuthorsFromACountry(int countryId)
    {
        var authors = _mapper.Map<List<AuthorDto>>(_countryRepository.GetAuthorsFromACountry(countryId));

        if(authors.Count < 1)
        {
            return NotFound(new { message = "There are no authors from this country" });
        }

        return Ok(authors);
    }

    [HttpPost]
    public IActionResult CreateCategory([FromBody] CountryDto countryToBeCreated)
    {
        if (countryToBeCreated == null)
        {
            return BadRequest(new { message = "Invalid country" });
        }

        var country = _countryRepository.GetCountries()
            .Where(c => c.Name.ToLower() == countryToBeCreated.Name.ToLower())
            .FirstOrDefault();

        if (country != null)
        {
            return Conflict(new { message = "Country already exists" });
        }

        var countryMap = _mapper.Map<Country>(countryToBeCreated);

        if (!_countryRepository.CreateCountry(countryMap))
        {
            return StatusCode(500, new { message = "Something went wrong while saving" });
        }

        return Ok("Successfully created");
    }

    [HttpPut("update/{countryId}")]
    public IActionResult UpdateCountry(int countryId, [FromBody] CountryDto updatedCountry)
    {
        if (updatedCountry == null)
        {
            return BadRequest(new { message = "Invalid country" });
        }

        if (updatedCountry.Id != countryId)
        {
            return BadRequest(new { message = "Ids don't match" });
        }

        if (!_countryRepository.CountryExists(countryId))
        {
            return NotFound(new { message = "Category with this Id doesn't exist" });
        }

        var countryMap = _mapper.Map<Country>(updatedCountry);

        if (!_countryRepository.UpdateCountry(countryMap))
        {
            return StatusCode(500, new { message = "Something went wrong while updating country" });
        }

        return Ok("Successfully updated");
    }

    [HttpDelete("{countryId}")]
    public IActionResult DeleteCountry(int countryId)
    {
        if (!_countryRepository.CountryExists(countryId))
        {
            return NotFound(new { message = "Country with this Id doesn't exist" });
        }

        var countryToDelete = _countryRepository.GetCountry(countryId);

        if (!_countryRepository.DeleteCountry(countryToDelete))
        {
            return StatusCode(500, new { message = "Something went wrong while deleting country" });
        }

        return Ok("Successfully deleted");
    }
}
