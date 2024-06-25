
using LibrarySystem.Application.Services.Interfaces;
using LibrarySystem.SharedKernel.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.WebAPI.Controllers
{
    [Route("api/loans")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoanDTO>>> GetLoans()
        {
            var loans = await _loanService.GetLoans();
            return Ok(loans);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LoanDTO>> GetLoan(int id)
        {
            var loan = await _loanService.GetLoanById(id);
            if (loan == null)
            {
                return NotFound();
            }
            return Ok(loan);
        }

        [HttpPost]
        public async Task<ActionResult<LoanDTO>> AddLoan(CreateLoanDTO loanDto)
        {
            var addedLoan = await _loanService.AddLoan(loanDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LoanDTO>> UpdateLoan(int id, UpdateLoanDTO loanDto)
        {
            var updatedLoan = await _loanService.UpdateLoan(id, loanDto);
            if (updatedLoan == null)
            {
                return NotFound();
            }
            return Ok(updatedLoan);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLoan(int id)
        {
            var success = await _loanService.DeleteLoan(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPut("{id}/complete")]
        public async Task<ActionResult<LoanDTO>> CompleteLoan(int id)
        {
            var completedLoan = await _loanService.CompleteLoan(id);
            if (completedLoan == null)
            {
                return NotFound();
            }
            return Ok(completedLoan);
        }
    }
}
