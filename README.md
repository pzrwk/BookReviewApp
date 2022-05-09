# Book Review Application

## **_Setup_**

1. Clone a repository

`git clone https://github.com/pzrwk/BookReviewApp.git`

2. Open it using Visual Studio
3. Create DB in your MySQL engine
4. Get the connection string and replace it in `appsettings.json`
5. Open Package Manager Console
6. Run `Update-Database` command
7. Open terminal and navigate into `BookReviewApp` directory
8. Run `dotnet run seeddata`
9. Build & run the application

You should see swagger page with all endpoints.

## **_Endpoints_**

### **Author**

#### Get methods:

`/api/Author` - returns all authors

`/api/Author/{authorId}` - returns author with a given id

`/api/Author/{authorId}/books` - returns list books of an author with given id

### Post methods:

`/api/Author?countryId=` - creates a new author (id should not be specified) assigned to a country of given id (country has to exist)

### Put methods:

`/api/Author/update/{authorId}` - updates author with a given id. **IMPORTANT** id has to be given as parameter and specified in body

### Delete methods:

`/api/Author/{authorId}` - deletes author with a given id

### **Book**

#### Get methods:

`/api/Book` - returns all books

`/api/Book/id/{bookId}` - returns book with a given id

`/api/Book/title/{bookTitle}` - returns book with a given title

`/api/Book/{bookId}/rating` - returns rating of a book based on reviews

### Post methods:

`/api/Book?authorId=&categoryId=` - creates a new book (id should not be specified) assigned to author and category of given ids (both have to exist)

### Put methods:

`/api/Book/update/{bookId}` - updates book with a given id. **IMPORTANT** bookId has to be given as parameter and specified in body

### Delete methods:

`/api/Book/{bookId}` - deletes book with a given id

### **Category**

#### Get methods:

`/api/Category` - returns all categories

`/api/Category/id/{categoryId}` - returns category with a given id

`/api/Category/name/{categoryName}` - returns category with a given name

`/api/Category/{categoryId}/books` - returns books from category with a given id

### Post methods:

`/api/Category` - creates a new category (id should not be specified)

### Put methods:

`/api/Category/update/{categoryId}` - updates category with a given id. **IMPORTANT** categoryId has to be given as parameter and specified in body

### Delete methods:

`/api/Category/{categoryId}` - deletes category with a given id

### **Country**

#### Get methods:

`/api/Country` - returns all countries

`/api/Country/id/{countryId}` - returns country with a given id

`/api/Country/name/{countryName}` - returns category with a given name

`/api/Country/author/{authorId}` - returns country of an author with a given id

`/api/Country/{countryId}/authors` - returns authors from a country with a given id

### Post methods:

`/api/Country` - creates a new country (id should not be specified)

### Put methods:

`/api/Country/update/{countryId}` - updates country with a given id. **IMPORTANT** countryId has to be given as parameter and specified in body

### Delete methods:

`/api/Country/{countryId}` - deletes country with a given id

### **Review**

#### Get methods:

`/api/Review` - returns all reviews

`/api/Review/{reviewId}` - returns review with a given id

`/api/Review/book/{bookId}` - returns reviews of book with a given id

`/api/Review/reviewer/{reviewerId}` - returns reviews of reviewer with a given id

### Post methods:

`/api/Review` - creates a new review (id should not be specified) assigned to reviewer and book of given ids (both have to exist)

### Put methods:

`/api/Review/update/{reviewId}` - updates review with a given id. **IMPORTANT** reviewId has to be given as parameter and specified in body

### Delete methods:

`/api/Review/{reviewId}` - deletes review with a given id

### **Reviewer**

#### Get methods:

`/api/Reviewer` - returns all reviewers

`/api/Reviewer/{reviewerId}` - returns reviewer with a given id

### Post methods:

`/api/Reviewer` - creates a new reviewer (id should not be specified)

### Put methods:

`/api/Reviewer/update/{reviewerId}` - updates reviewer with a given id. **IMPORTANT** reviewerId has to be given as parameter and specified in body

### Delete methods:

`/api/Reviewer/{reviewerId}` - deletes reviewer with a given id
