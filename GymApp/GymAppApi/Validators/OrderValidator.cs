using FluentValidation;
using GYM.API.Models;

namespace GYM.API.Validators
{
    public class OrderValidator : AbstractValidator<OrderViewModel>
    {
        public OrderValidator()
        {
            RuleFor(order => order.Title).NotNull().Length(1, 150);
            RuleFor(order => order.Description).NotNull().Length(1, 150);
            RuleFor(order => order.Cost).NotNull().GreaterThan(0);
            RuleFor(order => order.VisitorId).NotNull().GreaterThan(0);
        }
    }
}
