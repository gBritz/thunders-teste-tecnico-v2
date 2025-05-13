using FluentValidation;

namespace Thunders.TechTest.ApiService.Application.Tolls.RegisterPayment;

internal class RegisterPaymentValidation : AbstractValidator<RegisterPaymentCommand>
{
    public RegisterPaymentValidation()
    {
        RuleFor(_ => _.TollPlazaId)
          .NotEmpty();

        RuleFor(_ => _.PaidAt)
          .NotEmpty();

        RuleFor(_ => _.Amount)
          .GreaterThan(0)
          .LessThanOrEqualTo(1000);

        RuleFor(_ => _.Vehicle)
          .IsInEnum();
    }
}