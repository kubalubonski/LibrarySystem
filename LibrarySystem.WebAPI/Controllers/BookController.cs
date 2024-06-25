using LibrarySystem.Application.Services.Interfaces;
using LibrarySystem.SharedKernel.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.WebAPI.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            var books = await _bookService.GetBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBook(int id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> AddBook(BookDTO bookDto)
        {
            var addedBook = await _bookService.AddBook(bookDto);
            return Ok(addedBook);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDTO>> UpdateBook(int id, BookDTO bookDto)
        {
            var updatedBook = await _bookService.UpdateBook(id, bookDto);
            if (updatedBook == null)
            {
                return NotFound();
            }
            return Ok(updatedBook);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBook(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
