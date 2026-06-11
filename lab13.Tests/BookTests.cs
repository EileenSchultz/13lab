using System.Net.Http.Json;
using System.Net;
using lab13.Data;

namespace lab13.Tests;

public class BookTests: IClassFixture<ApiFactory>
{
    private readonly ApiFactory _factory;

    public BookTests(ApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task CreateBook()
    {
        // Arrange
        var client = _factory.CreateClient();
        var bookData = new
        {
            Name = "Tasldlds",
            Author = "F.F Aydfstds",
            ReleaseDate = "2024-01-15"
        };
        
        // Act
        var response = await client.PostAsJsonAsync("/books", bookData);
        var book = await response.Content.ReadFromJsonAsync<BookModel>();
        
        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(book);
        Assert.Equal("Tasldlds", book.Name);
        Assert.Equal("F.F Aydfstds", book.Author);
    }
    
    public async Task GetBook_ById()
    {
        // Arrange
        var client = _factory.CreateClient();
        var createResponse = await client.PostAsJsonAsync("/books", new 
        { 
            Name = "LALALALAL", 
            Author = "Psddksfkf",
            ReleaseDate = "2023-05-20"
        });
        var createdBook = await createResponse.Content.ReadFromJsonAsync<BookModel>();
        
        // Act
        var response = await client.GetAsync($"/books/{createdBook!.Id}");
        var book = await response.Content.ReadFromJsonAsync<BookModel>();
        
        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(book);
        Assert.Equal(createdBook.Id, book.Id);
    }
    
    [Fact]
    public async Task GetBookById_NotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("/books/99999");
        
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task UpdateBook()
    {
        // Arrange
        var client = _factory.CreateClient();
        var createResponse = await client.PostAsJsonAsync("/books", new 
        { 
            Name = "Old", 
            Author = "Old",
            ReleaseDate = "2020-01-01"
        });

        var createdBook = await createResponse.Content.ReadFromJsonAsync<BookModel>();
        var updateData = new 
        { 
            Name = "New", 
            Author = "New",
            ReleaseDate = "2026-12-31"
        };
        
        // Act
        var response = await client.PutAsJsonAsync($"/books/{createdBook!.Id}", updateData);
        var updatedBook = await response.Content.ReadFromJsonAsync<BookModel>();
        
        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(updatedBook);
        Assert.Equal("New", updatedBook.Name);
        Assert.Equal("New", updatedBook.Author);
    }
    
    [Fact]
    public async Task UpdateBook_NotFound()
    {
        // Arrange
        var updateData = new 
        { 
            Name = "Idk", 
            Author = "I.D.K Too",
            ReleaseDate = "2001-01-01"
        };

        // Act
        var response = await client.PutAsJsonAsync("/books/99999", updateData);
        
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task DeleteBook()
    {
        // Arrange
        var client = _factory.CreateClient();
        var createResponse = await client.PostAsJsonAsync("/books", new
        {
            Name = "Deleting",
            Author = "Author for delete",
            ReleaseDate = "2022-02-22"
        });
        var createdBook = await createResponse.Content.ReadFromJsonAsync<BookModel>();

        // Act
        var response = await client.DeleteAsync($"/books/{createdBook!.Id}");
        var getResponse = await client.GetAsync($"/books/{createdBook.Id}");
        
        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
    }
    
    [Fact]
    public async Task DeleteBook_NotFound()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.DeleteAsync("/books/99999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetAllBooks_Counts()
    {
        // Arrange
        var client = _factory.CreateClient();

        await client.PostAsJsonAsync("/books", new { Name = "Book 1", Author = "Stanford Pines", ReleaseDate = "2012-12-12" });
        await client.PostAsJsonAsync("/books", new { Name = "Book 2", Author = "Stanford Pines", ReleaseDate = "2022-02-02" });

        // Act
        var response = await client.GetAsync("/books");

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        var books = await response.Content.ReadFromJsonAsync<List<BookModel>>();
        Assert.NotNull(books);
        Assert.True(books.Count >= 2);
    }

    [Fact]
    public async Task GetAllBooks_Search()
    {
        // Arrange
        var client = _factory.CreateClient();

        await client.PostAsJsonAsync("/books", new { Name = "Jester Funny", Author = "K.K. Lalalend", ReleaseDate = "2025-06-06" });
        await client.PostAsJsonAsync("/books", new { Name = "How to rebuild a civilization", Author = "The Book", ReleaseDate = "2022-09-21" });

        // для поиска по названию
        // Act
        var response = await client.GetAsync("/books?search=Jester");
        var books = await response.Content.ReadFromJsonAsync<List<BookModel>>();

        // Assert
        Assert.NotNull(books);
        Assert.Single(books);
        Assert.Contains("Jester Funny", books[0].Name);

        // для поиска по автору
        // Act
        response = await client.GetAsync("/books?search=The Book");
        books = await response.Content.ReadFromJsonAsync<List<BookModel>>();

        // Assert
        Assert.NotNull(books);
        Assert.Single(books);
        Assert.Contains("The Book", books[0].Author);
    }

    [Fact]
    public async Task CreateBook_NoDate()
    {
        // Arrange
        var client = _factory.CreateClient();
        var bookData = new
        {
            Name = "no name",
            Author = "no author",
            ReleaseDate = (string?)null
        };

        // Act
        var response = await client.PostAsJsonAsync("/books", bookData);
        var book = await response.Content.ReadFromJsonAsync<BookModel>();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(book);
        Assert.Null(book.ReleaseDate);
    }
}