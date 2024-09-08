using AutoMapper;
using LibrarySystem.Application.Services.Interfaces;
using LibrarySystem.Domain.Exceptions;
using LibrarySystem.Domain.Models;
using LibrarySystem.SharedKernel.DTOs;
using Microsoft.Extensions.Logging;
using LibrarySystem.Domain.Concrats.Interfaces;

namespace LibrarySystem.Server.Application.Implementation
{
    public class ReaderService : IReaderService
    {
        private readonly IReaderRepository _readerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ReaderService> _logger;

        public ReaderService(IReaderRepository readerRepository, IMapper mapper, ILogger<ReaderService> logger)
        {
            _readerRepository = readerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ReaderDTO>> GetReaders()
        {
            var readers = await _readerRepository.GetReaders();

            if(readers == null) 
            {
                throw new NotFoundException("There are no readers in database");
            }
            var readerDtos = _mapper.Map<IEnumerable<ReaderDTO>>(readers);

            _logger.LogInformation("Successfully retrieved readers");

            return readerDtos;
        }

        public async Task<ReaderDTO> GetReaderById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Wrong id. It should be more than 0");
            }
            var reader = await _readerRepository.GetReaderById(id);
            if (reader == null)
            {
                throw new NotFoundException($"Reader with ID {id} not found");
            }
            var readerDto = _mapper.Map<ReaderDTO>(reader);

            _logger.LogInformation("Successfully retrieved reader with ID {id}", id);

            return readerDto;
        }

        public async Task<ReaderDTO> AddReader(ReaderDTO readerDto)
        {
            if (readerDto == null)
            {
                throw new BadRequestException("Reader DTO is null");
            }

            var reader = _mapper.Map<Reader>(readerDto);
            var addedReader = await _readerRepository.AddReader(reader);
            var addedReaderDto = _mapper.Map<ReaderDTO>(addedReader);

            _logger.LogInformation("Successfully added new reader");

            return addedReaderDto;
        }

        public async Task<ReaderDTO> UpdateReader(int id, ReaderDTO readerDto)
        {
            if (readerDto == null || id <= 0)
            {
                throw new BadRequestException("Wrong id or data format");
            }

            var readerToUpdate = await _readerRepository.GetReaderById(id);
            if (readerToUpdate == null)
            {
                throw new NotFoundException($"Reader with ID {id} not found");
            }

            _mapper.Map(readerDto, readerToUpdate);
            var updatedReader = await _readerRepository.UpdateReader(readerToUpdate);
            var updatedReaderDto = _mapper.Map<ReaderDTO>(updatedReader);

            _logger.LogInformation("Successfully updated reader with ID {id}", id);

            return updatedReaderDto;
        }

        public async Task<bool> DeleteReader(int id)
        {
            var readerToDelete = await _readerRepository.GetReaderById(id);
            if (readerToDelete == null)
            {
                throw new NotFoundException($"Reader with ID {id} not found");
            }

            await _readerRepository.DeleteReader(id);

            _logger.LogInformation("Successfully deleted reader with ID {id}", id);

            return true;
        }

        public async Task<IEnumerable<LoanDTO>> GetLoansByReaderId(int readerId)
        {
            if (readerId <= 0)
            {
                throw new BadRequestException("Wrong id. It should be more than 0");
            }
            var loans = await _readerRepository.GetLoansByReaderId(readerId);

            if(loans is null)
            {
                throw new Exception("This Reader has no any loans");
            }
            var loanDtos = _mapper.Map<IEnumerable<LoanDTO>>(loans);

            _logger.LogInformation("Successfully retrieved loans for reader with id = {0}", readerId);

            return loanDtos;
        }
        public async Task<IEnumerable<LoanDTO>> GetActiveLoansByReaderId(int readerId)
        {
            if (readerId <= 0)
            {
                throw new BadRequestException("Wrong id. It should be more than 0");
            }
            var activeLoans = await _readerRepository.GetActiveLoansByReaderId(readerId);
            if (activeLoans is null)
            {
                throw new Exception("This Reader has no any active loans");
            }
            var loanDtos = _mapper.Map<IEnumerable<LoanDTO>>(activeLoans);

            _logger.LogInformation("Successfully retrieved active loans for reader with id = {0}", readerId);

            return loanDtos;
        }

        public async Task<bool> ReaderExistsByName(string name)
        {
            return await _readerRepository.ReaderExistsByName(name);
        }
    }
}
