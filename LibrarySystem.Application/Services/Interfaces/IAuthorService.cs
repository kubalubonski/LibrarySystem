using LibrarySystem.SharedKernel.DTOs;

namespace LibrarySystem.Application.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorDTO>> GetAuthors();
        Task<AuthorDTO> GetAuthorById(int id);
        Task<AuthorDTO> AddAuthor(AuthorDTO authorDto);
        Task<AuthorDTO> UpdateAuthor(int id, AuthorDTO authorDto);
        Task<bool> DeleteAuthor(int id);
    }
}
