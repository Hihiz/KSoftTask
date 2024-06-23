using FluentValidation;
using KSoftTask.Application.Dto.PublisherBooks;

namespace KSoftTask.Application.Validations.FluentValidations.PublisherBooks
{
    public class UpdatePublisherBookValidator : AbstractValidator<UpdatePublisherBookDto>
    {
        public UpdatePublisherBookValidator()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage("Укажите номер издательства !");
            RuleFor(x => x.PublisherId)
             .NotEmpty().WithMessage("Издатель книги пустой !")
              .GreaterThan(0).WithMessage("Укажите издателя книгу!");
            RuleFor(x => x.BookId)
                .NotEmpty().WithMessage("Укажите книгу")
                 .GreaterThan(0).WithMessage("Укажите книгу !");
        }
    }
}
