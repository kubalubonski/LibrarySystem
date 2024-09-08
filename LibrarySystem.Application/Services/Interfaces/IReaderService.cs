using LibrarySystem.SharedKernel.DTOs;

namespace LibrarySystem.Application.Services.Interfaces
{
    public interface IReaderService
    {
        Task<IEnumerable<ReaderDTO>> GetReaders();
        Task<ReaderDTO> GetReaderById(int id);
        Task<ReaderDTO> AddReader(ReaderDTO readerDto);
        Task<ReaderDTO> UpdateReader(int id, ReaderDTO readerDto);
        Task<bool> DeleteReader(int id);
        Task<IEnumerable<LoanDTO>> GetLoansByReaderId(int readerId);
        Task<IEnumerable<LoanDTO>> GetActiveLoansByReaderId(int readerId);
        Task<bool> ReaderExistsByName(string name);
    }
}
