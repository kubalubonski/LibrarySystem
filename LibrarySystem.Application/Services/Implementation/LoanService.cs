using AutoMapper;
using LibrarySystem.Application.Services.Interfaces;
using LibrarySystem.Domain.Concrats.Interfaces;
using LibrarySystem.Domain.Exceptions;
using LibrarySystem.Domain.Models;
using LibrarySystem.SharedKernel.DTOs;
using Microsoft.Extensions.Logging;

namespace LibrarySystem.Application.Services.Implementation
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<LoanService> _logger;

        public LoanService(ILoanRepository loanRepository, IMapper mapper, ILogger<LoanService> logger)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<LoanDTO>> GetLoans()
        {
            var loans = await _loanRepository.GetLoans();
            if(loans is null)
            {
                throw new NotFoundException("There are no loans in database");
            }
            var loanDtos = _mapper.Map<IEnumerable<LoanDTO>>(loans);

            _logger.LogInformation("Successfully retrieved loans");

            return loanDtos;
        }

        public async Task<LoanDTO> GetLoanById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Wrong id. It should be more than 0");
            }
            var loan = await _loanRepository.GetLoanById(id);
            if (loan == null)
            {
                throw new NotFoundException($"Loan with ID {id} not found");
            }
            var loanDto = _mapper.Map<LoanDTO>(loan);

            _logger.LogInformation("Successfully retrieved loan with ID {id}", id);
            
            return loanDto;
        }

        public async Task<LoanDTO> AddLoan(CreateLoanDTO loanDto)
        {
            if (loanDto == null)
            {
                throw new BadRequestException("Loan DTO is null");
            }

            var loan = _mapper.Map<Loan>(loanDto);

            loan.LoanDate = DateTime.Now;
            var addedLoan = await _loanRepository.AddLoan(loan);
            
            var addedLoanDto = _mapper.Map<LoanDTO>(addedLoan);
            _logger.LogInformation("Successfully added new loan");
            return addedLoanDto;
        }

        public async Task<LoanDTO> UpdateLoan(int id, UpdateLoanDTO loanDto)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Wrong id. It should be more than 0");
            }

            var loanToUpdate = await _loanRepository.GetLoanById(id);
            if (loanToUpdate == null)
            {
                throw new NotFoundException($"Loan with ID {id} not found");
            }

            _mapper.Map(loanDto, loanToUpdate);
            var updatedLoan = await _loanRepository.UpdateLoan(loanToUpdate);
            var updatedLoanDto = _mapper.Map<LoanDTO>(updatedLoan);

            _logger.LogInformation("Successfully updated loan with id = {id}", id);
            
            return updatedLoanDto;
        }

        public async Task<bool> DeleteLoan(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Wrong id. It should be more than 0");
            }
            var loanToDelete = await _loanRepository.GetLoanById(id);
            if (loanToDelete == null)
            {
                throw new NotFoundException($"Loan with ID {id} not found");
            }

            await _loanRepository.DeleteLoan(id);

            _logger.LogInformation("Successfully deleted loan with ID {id}", id);
            
            return true;
        }

        public async Task<LoanDTO> CompleteLoan(int id)
        {
            var completedLoan = await _loanRepository.CompleteLoan(id);
            if (completedLoan == null)
            {
                throw new NotFoundException($"Loan with ID {id} not found");
            }

            var completedLoanDto = _mapper.Map<LoanDTO>(completedLoan);

            _logger.LogInformation("Successfully completed loan with ID {id}", id);

            return completedLoanDto;
        }
    }
}
