using LibrarySystem.Domain.Models;


namespace LibrarySystem.Domain.Concrats.Interfaces
{
    public interface IReaderRepository
    {
        Task<IEnumerable<Reader>> GetReaders();
        Task<Reader> GetReaderById(int id);
        Task<Reader> AddReader(Reader reader);
        Task<Reader> UpdateReader(Reader reader);
        Task DeleteReader(int id);
        Task<IEnumerable<Loan>> GetLoansByReaderId(int readerId);
        Task<IEnumerable<Loan>> GetActiveLoansByReaderId(int readerId);
    }
}
