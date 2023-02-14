using FluentValidation;
using GYM.API.Models;

namespace GYM.API.Validators
{
    public class VisitorValidator : AbstractValidator<VisitorViewModel>
    {
        public VisitorValidator()
        {
            RuleFor(visitor => visitor.FirstName).NotNull().Length(1, 150);
            RuleFor(visitor => visitor.LastName).NotNull().Length(1, 150);
        }
    }
}
