using AutoMapper;
using LibrarySystem.Application.Services.Interfaces;
using LibrarySystem.Domain.Concrats.Interfaces;
using LibrarySystem.Domain.Exceptions;
using LibrarySystem.Domain.Models;
using LibrarySystem.SharedKernel.DTOs;
using Microsoft.Extensions.Logging;


namespace LibrarySystem.Application.Services.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BookService> _logger;

        public object ModelState { get; private set; }

        public BookService(IBookRepository bookRepository, IMapper mapper, ILogger<BookService> logger)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IEnumerable<BookDTO>> GetBooks()
        {
            var books = await _bookRepository.GetBooks();

            if (books is null)
            {
                throw new NotFoundException("There are no books in library");
            }

            var bookDtos = _mapper.Map<IEnumerable<BookDTO>>(books);


            _logger.LogInformation("Successfully retrieved books");

            return bookDtos;
        }
        public async Task<BookDTO> GetBookById(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Wrong id. It should be more than 0");
            }
            var book = await _bookRepository.GetBookById(id);

            if (book is null) 
            {
                throw new NotFoundException("Book not found");
            }
           
            var bookDto = _mapper.Map<BookDTO>(book);

            _logger.LogInformation("Successfully retrieved book with id = {id}", id);

            return bookDto;
        }
        public async Task<BookDTO> AddBook(BookDTO bookDto)
        {
            if(bookDto == null)
            {
                throw new BadRequestException("Book to add is null");
            }

            var book = _mapper.Map<Book>(bookDto);

            
            var addedBook = await _bookRepository.AddBook(book);

            var addedBookDto = _mapper.Map<BookDTO>(addedBook);

            _logger.LogInformation("Successfully added new book");

            return addedBookDto;
        }
        public async Task<BookDTO> UpdateBook(int id, BookDTO bookDto)
        {
            if(bookDto is null || id <= 0)
            {
                throw new BadRequestException("Updated book is null");
            }

            var bookToUpdate = await _bookRepository.GetBookById(id);
            if (bookToUpdate == null)
            {
                var message = $"Book with ID {id} not found.";
                throw new NotFoundException(message);
            }

            bookToUpdate.Title = bookDto.Title;
            bookToUpdate.Year = bookDto.Year;
            bookToUpdate.Author.Name = bookDto.Author.Name;
            
            var updatedBook = await _bookRepository.UpdateBook(bookToUpdate);
            var updatedBookDto = _mapper.Map<BookDTO>(updatedBook);

            _logger.LogInformation("Successfully update book with id = {id}", id);
            
            return updatedBookDto;
        }
        public async Task<bool> DeleteBook(int id)
        {
            if (id <= 0)
            {
                throw new BadRequestException("Wrong id. It should be more than 0");
            }
            var bookToDelete = await _bookRepository.GetBookById(id);
            if (bookToDelete == null)
            {
                var message = $"Book with ID {id} not found.";
                throw new NotFoundException(message);
            }

            await _bookRepository.DeleteBook(id);
            _logger.LogInformation("Successfully delete book with id = {id}", id);
            return true;
        }

    }
}
