using FluentValidation;
using KSoftTask.Application.Dto.Books;

namespace KSoftTask.Application.Validations.FluentValidations.Books
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookValidator()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage("Укажите номер книги");
            RuleFor(x => x.Title)
                 .NotEmpty().WithMessage("Введите название книги")
                 .MinimumLength(3).WithMessage("Название книги короткое");
        }
    }
}
