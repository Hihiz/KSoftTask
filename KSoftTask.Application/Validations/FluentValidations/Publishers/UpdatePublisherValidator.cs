using FluentValidation;
using KSoftTask.Application.Dto.Publishers;

namespace KSoftTask.Application.Validations.FluentValidations.Publishers
{
    public class UpdatePublisherValidator : AbstractValidator<UpdatePublisherDto>
    {
        public UpdatePublisherValidator()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage("Укажите номер издательства");
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Введите название издательства")
                .MinimumLength(3).WithMessage("Название издательства короткое");
        }
    }
}
