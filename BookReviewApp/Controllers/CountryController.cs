using AutoMapper;
using BookReviewApp.Dto;
using BookReviewApp.Interfaces;
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

    [HttpGet("authors/{authorId}")]
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
}
