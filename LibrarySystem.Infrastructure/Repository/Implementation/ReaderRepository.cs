using LibrarySystem.Domain.Models;
using LibrarySystem.Domain.Concrats.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Domain.Concrats.Implementation
{
    public class ReaderRepository : IReaderRepository
    {
        private readonly LibraryDbContext _context;

        public ReaderRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reader>> GetReaders()
        {
            return await _context.Readers.Include(r => r.Loans).ToListAsync();
        }

        public async Task<Reader> GetReaderById(int id)
        {
            return await _context.Readers.Include(r => r.Loans).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Reader> AddReader(Reader reader)
        {
            await _context.Readers.AddAsync(reader);
            await _context.SaveChangesAsync();
            return reader;
        }

        public async Task<Reader> UpdateReader(Reader reader)
        {
            _context.Readers.Update(reader);
            await _context.SaveChangesAsync();
            return reader;
        }

        public async Task DeleteReader(int id)
        {
            var reader = await _context.Readers.FindAsync(id);
            if (reader != null)
            {
                _context.Readers.Remove(reader);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Loan>> GetLoansByReaderId(int readerId)
        {
            return await _context.Loans
                                 .Where(l => l.ReaderId == readerId)
                                 .Include(l => l.Book)
                                 .ToListAsync();
        }
        public async Task<IEnumerable<Loan>> GetActiveLoansByReaderId(int readerId)
        {
            return await _context.Loans
                                 .Where(l => l.ReaderId == readerId && l.ReturnDate == null)
                                 .Include(l => l.Book)
                                 .ToListAsync();
        }
        public async Task<bool> ReaderExistsByName(string name)
        {
            return await _context.Readers.AnyAsync(r => r.Name == name);
        }
    }
}
