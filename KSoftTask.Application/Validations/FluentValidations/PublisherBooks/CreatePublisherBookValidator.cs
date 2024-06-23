using FluentValidation;
using KSoftTask.Application.Dto.PublisherBooks;

namespace KSoftTask.Application.Validations.FluentValidations.PublisherBooks
{
    public class CreatePublisherBookValidator : AbstractValidator<CreatePublisherBookDto>
    {
        public CreatePublisherBookValidator() 
        {         
            RuleFor(x => x.PublisherId)
              .NotEmpty().WithMessage("Издатель книги пустой !")
               .GreaterThan(0).WithMessage("Укажите издателя книгу!");
            RuleFor(x => x.BookId)
                .NotEmpty().WithMessage("Укажите книгу")
                 .GreaterThan(0).WithMessage("Укажите книгу !");
        }
    }
}
