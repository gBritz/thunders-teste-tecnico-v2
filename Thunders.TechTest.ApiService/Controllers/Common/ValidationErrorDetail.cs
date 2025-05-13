using FluentValidation.Results;

namespace Thunders.TechTest.ApiService.Controllers.Common;

public class ValidationErrorDetail
{
    public required string Error { get; init; } = string.Empty;
    public required string Detail { get; init; } = string.Empty;

    public static explicit operator ValidationErrorDetail(ValidationFailure validationFailure) =>
        ConvertFrom(validationFailure);

    public static ValidationErrorDetail ConvertFrom(ValidationFailure validationFailure)
    {
        return new ValidationErrorDetail
        {
            Detail = validationFailure.ErrorMessage,
            Error = validationFailure.ErrorCode
        };
    }
}