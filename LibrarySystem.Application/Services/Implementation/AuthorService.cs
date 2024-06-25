using AutoMapper;
using LibrarySystem.Application.Services.Interfaces;
using LibrarySystem.Domain.Concrats.Interfaces;
using LibrarySystem.Domain.Exceptions;
using LibrarySystem.Domain.Models;
using LibrarySystem.SharedKernel.DTOs;
using Microsoft.Extensions.Logging;


namespace LibrarySystem.Application.Services.Implementation
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorService> _logger;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper, ILogger<AuthorService> logger)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<AuthorDTO>> GetAuthors()
        {
            var authors = await _authorRepository.GetAuthors();

            if (authors == null || !authors.Any())
            {
                throw new NotFoundException("There are no authors in the database");
            }

            var authorDtos = _mapper.Map<IEnumerable<AuthorDTO>>(authors);
            _logger.LogInformation("Successfully retrieved authors");

            return authorDtos;
        }

        public async Task<AuthorDTO> GetAuthorById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Wrong id. It should be more than 0");
            }
            var author = await _authorRepository.GetAuthorById(id);
            if (author == null)
            {
                throw new NotFoundException($"Author with ID {id} not found");
            }
            var authorDto = _mapper.Map<AuthorDTO>(author);

            _logger.LogInformation("Successfully retrieved author with ID {id}", id);

            return authorDto;
        }

        public async Task<AuthorDTO> AddAuthor(AuthorDTO authorDto)
        {
            if (authorDto == null)
            {
                _logger.LogError("AuthorDto is null");
                throw new BadRequestException("Author to add is null");
            }
            var author = _mapper.Map<Author>(authorDto);

            var addedAuthor = await _authorRepository.AddAuthor(author);
            var addedAuthorDto = _mapper.Map<AuthorDTO>(addedAuthor);

            _logger.LogInformation("Successfully added new author");

            return addedAuthorDto;
        }

        public async Task<AuthorDTO> UpdateAuthor(int id, AuthorDTO authorDto)
        {
            if(authorDto == null || id <= 0)
            {
                throw new BadRequestException("Wrong id or data format");
            }
            var authorToUpdate = await _authorRepository.GetAuthorById(id);

            if (authorToUpdate == null)
            {
                throw new NotFoundException($"Author with ID {id} not found");
            }

            _mapper.Map(authorDto, authorToUpdate);
            var updatedAuthor = await _authorRepository.UpdateAuthor(authorToUpdate);
            var updatedAuthorDto = _mapper.Map<AuthorDTO>(updatedAuthor);

            _logger.LogInformation("Successfully updated author with ID {id}", id);

            return updatedAuthorDto;
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            if(id <= 0)
            {
                throw new BadRequestException("Wrong id. It should be more than 0");
            }
               
            var authorToDelete = await _authorRepository.GetAuthorById(id);

            if (authorToDelete == null)
            {
                throw new NotFoundException($"Author with ID {id} not found");
            }

            await _authorRepository.DeleteAuthor(id);

            _logger.LogInformation("Successfully deleted author with ID {id}", id);

            return true;
        }
    }
}
