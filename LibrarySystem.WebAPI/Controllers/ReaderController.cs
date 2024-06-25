using LibrarySystem.Application.Services.Interfaces;
using LibrarySystem.SharedKernel.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.WebAPI.Controllers
{
    [Route("api/readers")]
    [ApiController]
    public class ReadersController : ControllerBase
    {
        private readonly IReaderService _readerService;

        public ReadersController(IReaderService readerService)
        {
            _readerService = readerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReaderDTO>>> GetReaders()
        {
            var readers = await _readerService.GetReaders();
            return Ok(readers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReaderDTO>> GetReader(int id)
        {
            var reader = await _readerService.GetReaderById(id);
            if (reader == null)
            {
                return NotFound();
            }
            return reader;
        }

        [HttpPost]
        public async Task<ActionResult<ReaderDTO>> AddReader(ReaderDTO readerDto)
        {
            var addedReader = await _readerService.AddReader(readerDto);
            return Ok(addedReader);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReader(int id, ReaderDTO readerDto)
        {
            var updatedReader = await _readerService.UpdateReader(id, readerDto);
            if (updatedReader == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReader(int id)
        {
            var result = await _readerService.DeleteReader(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("{id}/loans")]
        public async Task<ActionResult<IEnumerable<LoanDTO>>> GetLoansByReaderId(int id)
        {
            var loans = await _readerService.GetLoansByReaderId(id);
            return Ok(loans);
        }

        [HttpGet("{id}/active-loans")]
        public async Task<ActionResult<IEnumerable<LoanDTO>>> GetActiveLoansByReaderId(int id)
        {
            var activeLoans = await _readerService.GetActiveLoansByReaderId(id);
            return Ok(activeLoans);
        }
    }
}
