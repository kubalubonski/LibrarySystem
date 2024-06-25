using LibrarySystem.Domain.Models;

namespace LibrarySystem.Domain.Concrats.Interfaces
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetLoans();
        Task<Loan> GetLoanById(int id);
        Task<Loan> AddLoan(Loan loan);
        Task<Loan> UpdateLoan(Loan loan);
        Task DeleteLoan(int id);
        Task<Loan> CompleteLoan(int id);
    }
}
