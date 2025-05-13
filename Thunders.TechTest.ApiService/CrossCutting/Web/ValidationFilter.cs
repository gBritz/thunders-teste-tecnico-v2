using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Filters;
using Thunders.TechTest.ApiService.CrossCutting.Validations;

namespace Thunders.TechTest.ApiService.CrossCutting.Web;

/// <summary>
///   Define um filtro para geração de notificações geradas no processamento da requisição.
/// </summary>
/// <seealso cref="IAsyncResultFilter" />
public class ValidationFilter : IAsyncResultFilter
{
    private readonly ValidationContext validationContext;

    /// <summary>
    ///   Initializes a new instance of the <see cref="ValidationFilter" /> class.
    /// </summary>
    /// <param name="notificationContext">The notification context.</param>
    public ValidationFilter(ValidationContext notificationContext)
    {
        validationContext = notificationContext;
    }

    /// <summary>
    ///   Called asynchronously before the action result.
    /// </summary>
    /// <param name="context">The ResultExecutingContext.</param>
    /// <param name="next">The ResultExecutionDelegate. Invoked to execute the next result filter or the result itself.</param>
    /// <returns>
    ///   A Task that on completion indicates the filter has executed.
    /// </returns>
    public Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (validationContext.HasValidations)
        {
            context.HttpContext.Response.ContentType = MediaTypeNames.Application.Json;
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            var notifications = JsonSerializer.Serialize(
              validationContext.ValidationMessages,
              options: new()
              {
                  PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
              });

            return context.HttpContext.Response.WriteAsync(notifications);
        }

        return next();
    }
}