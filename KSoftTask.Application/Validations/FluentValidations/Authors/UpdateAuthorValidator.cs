using FluentValidation;
using KSoftTask.Application.Dto.Authors;

namespace KSoftTask.Application.Validations.FluentValidations.Authors
{
    public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorDto>
    {
        public UpdateAuthorValidator()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage("Укажите номер автора !");
            RuleFor(x => x.Name)
             .NotEmpty().WithMessage("Введите автора книги")
             .MinimumLength(3).WithMessage("Имя автора короткое !");
        }
    }
}
