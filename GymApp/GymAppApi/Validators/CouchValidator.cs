using FluentValidation;
using GYM.API.Models;

namespace GYM.API.Validators
{
    public class CouchValidator : AbstractValidator<CouchViewModel>
    {
        public CouchValidator()
        {
            RuleFor(couch => couch.FirstName).NotNull().Length(1, 150);
            RuleFor(couch => couch.LastName).NotNull().Length(1, 150);
            RuleFor(couch => couch.Description).NotNull().Length(1, 750);
        }
    }
}
