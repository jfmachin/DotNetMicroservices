using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder {
    public class UpdateOrderCommandValidator : AbstractValidator<CheckoutOrderCommand> {
        public UpdateOrderCommandValidator() {
            RuleFor(p => p.UserName)
                .NotEmpty()
                .WithMessage("{UserName} is required")
                .NotNull()
                .MaximumLength(50)
                .WithMessage("{UserName} must no exceed 50 chars");

            RuleFor(p => p.EmailAddress)
                .NotEmpty()
                .WithMessage("{EmailAddress} is required");

            RuleFor(p => p.TotalPrice)
                .NotEmpty()
                .WithMessage("{TotalPrice} is required")
                .GreaterThan(0)
                .WithMessage("{TotalPrice} should be greater than zero");
        }
    }
}
