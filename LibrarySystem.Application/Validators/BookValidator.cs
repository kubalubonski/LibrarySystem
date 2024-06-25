using FluentValidation;
using LibrarySystem.SharedKernel.DTOs;


namespace LibrarySystem.Application.Validators
{
    public class BookValidator : AbstractValidator<BookDTO>
    {
        public BookValidator() 
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Year).GreaterThan(0).WithMessage("Year must be greater than 0");
        }
    }
}
