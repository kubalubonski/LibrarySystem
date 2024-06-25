using LibrarySystem.Domain.Models;
using LibrarySystem.Domain.Concrats.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Domain.Concrats.Implementation
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryDbContext _context;

        public LoanRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Loan>> GetLoans()
        {
            return await _context.Loans
                .Include(l => l.Book)
                .ThenInclude(a => a.Author)
                .Include(l => l.Reader)
                .ToListAsync();
        }

        public async Task<Loan> GetLoanById(int id)
        {
            return await _context.Loans
                .Include(l => l.Book)
                .ThenInclude(a => a.Author)
                .Include(l => l.Reader)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Loan> AddLoan(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task<Loan> UpdateLoan(Loan loan)
        {
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task DeleteLoan(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            if (loan != null)
            {
                _context.Loans.Remove(loan);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Loan> CompleteLoan(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            if (loan != null)
            {
                loan.ReturnDate = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
            return loan;
        }
    }
}
