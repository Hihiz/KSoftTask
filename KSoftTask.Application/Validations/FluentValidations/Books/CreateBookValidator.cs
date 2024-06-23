using FluentValidation;
using KSoftTask.Application.Dto.Books;

namespace KSoftTask.Application.Validations.FluentValidations.Books
{
    public class CreateBookValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Введите название книги")
                .MinimumLength(3).WithMessage("Название книги короткое");
        }
    }
}
