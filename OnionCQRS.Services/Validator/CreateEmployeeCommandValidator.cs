using FluentValidation;
using OnionCQRS.Core.Command;

namespace OnionCQRS.Services.Validator
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.FirstName).Length(0, 10);
        }
    }
}
