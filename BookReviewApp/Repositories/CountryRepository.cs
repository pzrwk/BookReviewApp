using BookReviewApp.Data;
using BookReviewApp.Interfaces;
using BookReviewApp.Models;

namespace BookReviewApp.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly DataContext _context;

    public CountryRepository(DataContext context)
    {
        _context = context;
    }

    public ICollection<Country> GetCountries()
    {
        return _context.Countries.OrderBy(c => c.Id).ToList();
    }

    public Country GetCountry(int id)
    {
        return _context.Countries.Where(c => c.Id == id).FirstOrDefault();
    }

    public Country GetCountry(string name)
    {
        return _context.Countries.Where(c => c.Name.Equals(name)).FirstOrDefault();
    }
    public bool CountryExists(int id)
    {
        return _context.Countries.Any(c => c.Id == id);
    }

    public bool CountryExists(string name)
    {
        return _context.Countries.Any(c => c.Name.Equals(name));
    }

    public Country GetCountryByAuthor(int authorId)
    {
        return _context.Authors.Where(a => a.Id == authorId).Select(c => c.Country).FirstOrDefault();
    }

    public ICollection<Author> GetAuthorsFromACountry(int countryId)
    {
        return _context.Authors.Where(a => a.Country.Id == countryId).ToList();
    }

    public bool CreateCountry(Country country)
    {
        _context.Add(country);
        return Save();
    }

    public bool Save()
    {
        var save = _context.SaveChanges();

        return save > 0 ? true : false;
    }

    public bool UpdateCountry(Country country)
    {
        _context.Update(country);
        return Save();
    }

    public bool DeleteCountry(Country country)
    {
        _context.Remove(country);
        return Save();
    }
}
