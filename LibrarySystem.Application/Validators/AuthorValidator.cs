using FluentValidation;
using LibrarySystem.SharedKernel.DTOs;


namespace LibrarySystem.Application.Validators
{
    public class AuthorValidator : AbstractValidator<AuthorDTO>
    {
        public AuthorValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name of author is required");
        }
    }
}
