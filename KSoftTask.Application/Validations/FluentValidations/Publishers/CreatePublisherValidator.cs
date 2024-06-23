using FluentValidation;
using KSoftTask.Application.Dto.Publishers;

namespace KSoftTask.Application.Validations.FluentValidations.Publishers
{
    public class CreatePublisherValidator : AbstractValidator<CreatePublisherDto>
    {
        public CreatePublisherValidator()
        {
            RuleFor(x => x.Title)
                  .NotEmpty().WithMessage("Введите название издательства")
                  .MinimumLength(3).WithMessage("Название издательства короткое");
        }
    }
}
