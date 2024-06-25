using FluentValidation;
using LibrarySystem.SharedKernel.DTOs;

namespace LibrarySystem.Application.Validators
{
    public class CreateLoanDTOValidator : AbstractValidator<CreateLoanDTO>
    {
        public CreateLoanDTOValidator()
        {
            RuleFor(dto => dto.BookId)
                .NotEmpty().WithMessage("Book ID is required")
                .GreaterThan(0).WithMessage("Book ID must be greater than 0");

            RuleFor(dto => dto.ReaderId)
                .NotEmpty().WithMessage("Reader ID is required")
                .GreaterThan(0).WithMessage("Reader ID must be greater than 0");
          
        }

       
    }
    public class UpdateLoanDTOValidator : AbstractValidator<UpdateLoanDTO>
    {
        public UpdateLoanDTOValidator()
        {
            RuleFor(dto => dto.BookId)
                .NotEmpty().WithMessage("Book ID is required")
                .GreaterThan(0).WithMessage("Book ID must be greater than 0");

            RuleFor(dto => dto.ReaderId)
                .NotEmpty().WithMessage("Reader ID is required")
                .GreaterThan(0).WithMessage("Reader ID must be greater than 0");
        }
    }

}
