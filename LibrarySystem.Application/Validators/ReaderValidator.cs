using FluentValidation;
using LibrarySystem.SharedKernel.DTOs;

namespace LibrarySystem.Application.Validators
{
    public class ReaderValidator : AbstractValidator<ReaderDTO>
    {
        public ReaderValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Username is required");
        }
    }
}
