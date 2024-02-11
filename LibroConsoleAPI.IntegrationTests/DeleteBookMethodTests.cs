using LibroConsoleAPI.Business.Contracts;
using LibroConsoleAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibroConsoleAPI.IntegrationTests.XUnit
{
    public class DeleteBookMethodTests: IClassFixture<BookManagerFixture>
    {
        private readonly BookManagerFixture _fixture;
        private readonly IBookManager _bookManager;
        private readonly TestLibroDbContext _dbContext;

        public DeleteBookMethodTests()
        {
            _fixture = new BookManagerFixture();
            _bookManager = _fixture.BookManager;
            _dbContext = _fixture.DbContext;
        }
        [Fact]
        public async Task DeleteBookAsync_WithValidISBN_ShouldRemoveBookFromDb()
        {
            // Arrange: Add a book with a valid ISBN to the database
            var newBook = new Book
            {
                Title = "Test Book",
                Author = "John Doe",
                ISBN = "1234567890123", // Example ISBN
                YearPublished = 2021,
                Genre = "Fiction",
                Pages = 100,
                Price = 19.99
            };
            await _bookManager.AddAsync(newBook);

            // Act: Delete the book with the valid ISBN
            await _bookManager.DeleteAsync("1234567890123");

            // Assert: Verify that the book is removed from the database
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _bookManager.GetSpecificAsync("1234567890123"));
        }
    }
}
