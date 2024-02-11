using LibroConsoleAPI.Business.Contracts;
using LibroConsoleAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibroConsoleAPI.IntegrationTests.XUnit
{
    public class AddBookMethodTests : IClassFixture<BookManagerFixture>
    {
        private readonly BookManagerFixture _fixture;
        private readonly IBookManager _bookManager;
        private readonly TestLibroDbContext _dbContext;

        public AddBookMethodTests()
        {
            _fixture = new BookManagerFixture();
            _bookManager = _fixture.BookManager;
            _dbContext = _fixture.DbContext;
        }

        //Positive Test
        [Fact]
        public async Task AddBookAsync_ShouldAddBook() 
        {
            // Arrange
            var newBook = new Book
            {
                Title = "Test Book",
                Author = "John Doe",
                ISBN = "1234567890123",
                YearPublished = 2021,
                Genre = "Fiction",
                Pages = 100,
                Price = 19.99
            };

            // Act
            await _bookManager.AddAsync(newBook);

            // Assert
            var bookInDbUsingBookmanager = _bookManager.GetSpecificAsync(newBook.ISBN);
            var bookInDb = await _dbContext.Books.FirstOrDefaultAsync(b => b.ISBN == newBook.ISBN);
            Assert.NotNull(bookInDb);
            Assert.Equal("Test Book", bookInDb.Title);
            Assert.Equal("John Doe", bookInDb.Author);
            Assert.Equal(100, bookInDb.Pages);
            Assert.Equal(19.99, bookInDb.Price);
            Assert.Equal("Fiction", bookInDb.Genre);
            Assert.Equal(2021, bookInDb.YearPublished);
        }
    }
}
