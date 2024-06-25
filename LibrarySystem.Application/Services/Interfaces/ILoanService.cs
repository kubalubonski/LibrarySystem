using LibrarySystem.SharedKernel.DTOs;

namespace LibrarySystem.Application.Services.Interfaces
{
    public interface ILoanService
    {
        Task<IEnumerable<LoanDTO>> GetLoans();
        Task<LoanDTO> GetLoanById(int id);
        Task<LoanDTO> AddLoan(CreateLoanDTO loanDto);
        Task<LoanDTO> UpdateLoan(int id, UpdateLoanDTO loanDto);
        Task<bool> DeleteLoan(int id);
        Task<LoanDTO> CompleteLoan(int id);
    }
}
