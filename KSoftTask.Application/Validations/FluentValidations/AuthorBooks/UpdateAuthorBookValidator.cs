using FluentValidation;
using KSoftTask.Application.Dto.AuthorBooks;

namespace KSoftTask.Application.Validations.FluentValidations.AuthorBooks
{
    public class UpdateAuthorBookValidator : AbstractValidator<UpdateAuthorBookDto>
    {
        public UpdateAuthorBookValidator()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage("Укажите номер автора книг");
            RuleFor(x => x.AuthorId)
              .NotEmpty().WithMessage("Укажите автора книги")
               .GreaterThan(0).WithMessage("Выберите автора книги!");
            RuleFor(x => x.BookId)
                .NotEmpty().WithMessage("Укажите книгу")
                 .GreaterThan(0).WithMessage("Выберите книгу !");
        }
    }
}
