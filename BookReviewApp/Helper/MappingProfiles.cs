using AutoMapper;
using BookReviewApp.Dto;
using BookReviewApp.Models;

namespace BookReviewApp.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Book, BookDto>();
        CreateMap<Country, CountryDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<Author, AuthorDto>();
        CreateMap<Review, ReviewDto>();
        CreateMap<Reviewer, ReviewerDto>();
        CreateMap<CategoryDto, Category>();
        CreateMap<CountryDto, Country>();
        CreateMap<AuthorDto, Author>();
        CreateMap<BookDto, Book>();
        CreateMap<ReviewDto, Review>();
        CreateMap<ReviewerDto, Reviewer>();
    }
}
