using BookReviewApp.Models;

namespace BookReviewApp.Interfaces;

public interface ICountryRepository
{
    ICollection<Country> GetCountries();
    Country GetCountry(int id);
    Country GetCountry(string name);
    bool CountryExists(int id);
    bool CountryExists(string name);
    Country GetCountryByAuthor(int authorId);
    ICollection<Author> GetAuthorsFromACountry(int countryId);
}
