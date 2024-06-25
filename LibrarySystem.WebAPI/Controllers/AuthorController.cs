using Microsoft.AspNetCore.Mvc;
using LibrarySystem.Application.Services.Interfaces;
using LibrarySystem.SharedKernel.DTOs;

namespace LibrarySystem.WebAPI.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors()
        {
            var authors = await _authorService.GetAuthors();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDTO>> GetAuthor(int id)
        {
            var author = await _authorService.GetAuthorById(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> AddAuthor(AuthorDTO authorDto)
        {
            var addedAuthor = await _authorService.AddAuthor(authorDto);
            return Ok(addedAuthor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AuthorDTO>> UpdateAuthor(int id, AuthorDTO authorDto)
        {
            var updatedAuthor = await _authorService.UpdateAuthor(id, authorDto);
            if (updatedAuthor == null)
            {
                return NotFound();
            }
            return Ok(updatedAuthor);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAuthor(int id)
        {
            var result = await _authorService.DeleteAuthor(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
