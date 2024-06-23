using FluentValidation;
using KSoftTask.Application.Dto.Authors;

namespace KSoftTask.Application.Validations.FluentValidations.Authors
{
    public class CreateAuthorValidator : AbstractValidator<CreateAuthorDto>
    {
        public CreateAuthorValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Введите автора книги")
               .MinimumLength(3).WithMessage("Имя автора короткое !");
        }
    }
}
