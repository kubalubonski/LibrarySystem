using LibrarySystem.SharedKernel.DTOs;

namespace LibrarySystem.Application.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> GetBooks();
        Task<BookDTO> GetBookById(int id);
        Task<BookDTO> AddBook(BookDTO bookDto);
        Task<BookDTO> UpdateBook(int id, BookDTO bookDto);
        Task<bool> DeleteBook(int id);
    }
}
