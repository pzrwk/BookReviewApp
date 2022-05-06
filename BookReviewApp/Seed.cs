using BookReviewApp.Data;
using BookReviewApp.Models;

namespace BookReviewApp;

public class Seed
{
    private readonly DataContext _context;

    public Seed(DataContext context)
    {
        _context = context;
    }

    public void SeedDataContext()
    {
        if(!_context.BookAuthors.Any())
        {
            var bookAuthors = new List<BookAuthor>()
            {
                new BookAuthor()
                {
                    Book = new Book()
                    {
                        ISBN = "985-58-38592",
                        Title = "Pan Tadeusz",
                        ReleaseDate = new DateTime(1831, 1, 1),
                        CopiesSold = 2000000,
                        Reviews = new List<Review>()
                        {
                            new Review
                            {
                                Title = "Pan Tadeusz",
                                Text = "Pan Tadeusz is a wonderful epica poema.",
                                Rating = 5,
                                Reviewer = new Reviewer { FirstName = "Jan", LastName = "Kowalski"}
                            },
                            new Review
                            {
                                Title = "Pan Tadeusz",
                                Text = "Pan Tadeusz is my favourite book.",
                                Rating = 5,
                                Reviewer = new Reviewer { FirstName = "Adam", LastName = "Nowak"}
                            },
                            new Review
                            {
                                Title = "Pan Tadeusz",
                                Text = "Pan Tadeusz is really boring.",
                                Rating = 1,
                                Reviewer = new Reviewer { FirstName = "Wiesław", LastName = "Morgan"}
                            }
                        },
                        BookCategories = new List<BookCategory>()
                        {
                            new BookCategory() { Category = new Category() { Name = "Poema" }}
                        }

                    },
                    Author = new Author()
                    {
                        FirstName = "Adam",
                        LastName = "Mickiewicz",
                        Country = new Country() {
                            Name = "Poland"
                        }
                    }
                },
                new BookAuthor()
                {
                    Book = new Book()
                    {
                        ISBN = "979-84-38289",
                        Title = "Harry Potter",
                        ReleaseDate = new DateTime(1997, 6, 27),
                        CopiesSold = 200000000,
                        Reviews = new List<Review>()
                        {
                            new Review
                            {
                                Title = "Harry Potter",
                                Text = "Harry Potter is a wonderful epica poema.",
                                Rating = 5,
                                Reviewer = new Reviewer { FirstName = "Jan", LastName = "Kowalski"}
                            },
                            new Review
                            {
                                Title = "Harry Potter",
                                Text = "Harry Potter is really boring.",
                                Rating = 1,
                                Reviewer = new Reviewer { FirstName = "Adam", LastName = "Nowak"}
                            },
                            new Review
                            {
                                Title = "Harry Potter",
                                Text = "Harry Potter is my favourite book.",
                                Rating = 5,
                                Reviewer = new Reviewer { FirstName = "Wiesław", LastName = "Morgan"}
                            }
                        },
                        BookCategories = new List<BookCategory>()
                        {
                            new BookCategory() { Category = new Category() { Name = "Fantasy" }}
                        }

                    },
                    Author = new Author()
                    {
                        FirstName = "J.K.",
                        LastName = "Rowling",
                        Country = new Country() {
                            Name = "Great Britain"
                        }
                    }
                },
                new BookAuthor()
                {
                    Book = new Book()
                    {
                        ISBN = "978-58-58290",
                        Title = "Romeo & Juliet",
                        ReleaseDate = new DateTime(1851, 2, 12),
                        CopiesSold = 5000000,
                        Reviews = new List<Review>()
                        {
                            new Review
                            {
                                Title = "Romeo & Juliet",
                                Text = "Romeo & Juliet is a wonderful epica poema.",
                                Rating = 5,
                                Reviewer = new Reviewer { FirstName = "Jan", LastName = "Kowalski"}
                            },
                            new Review
                            {
                                Title = "Romeo & Juliet",
                                Text = "Romeo & Juliet is my favourite book.",
                                Rating = 5,
                                Reviewer = new Reviewer { FirstName = "Adam", LastName = "Nowak"}
                            },
                            new Review
                            {
                                Title = "Romeo & Juliet",
                                Text = "Romeo & Juliet is really boring.",
                                Rating = 1,
                                Reviewer = new Reviewer { FirstName = "Wiesław", LastName = "Morgan"}
                            }
                        },
                        BookCategories = new List<BookCategory>()
                        {
                            new BookCategory() { Category = new Category() { Name = "Romance" }}
                        }

                    },
                    Author = new Author()
                    {
                        FirstName = "William",
                        LastName = "Shakespeare",
                        Country = new Country() {
                            Name = "England"
                        }
                    }
                },
            };
            _context.BookAuthors.AddRange(bookAuthors);
            _context.SaveChanges();
        }
    }
}
