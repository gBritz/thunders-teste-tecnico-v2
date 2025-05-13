using FluentValidation;
using FluentValidation.Results;
using Thunders.TechTest.ApiService.CrossCutting.Mediator.Abstractions;
using Thunders.TechTest.ApiService.CrossCutting.Validations;

namespace Infrastructure.Domain.Mediator
{
    /// <summary>
    /// Mediator of commands and handlers using dotnet <see cref="IServiceProvider"/> to activate new handler instance.
    /// </summary>
    /// <param name="serviceProvider">Native <see cref="IServiceProvider"/></param>
    /// <param name="validationContext">Context of validation</param>
    internal class ServiceProviderMediator(
    IServiceProvider serviceProvider,
    ValidationContext validationContext) : IMediator
    {
        private readonly IServiceProvider _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        private readonly ValidationContext _validationContext = validationContext ?? throw new ArgumentNullException(nameof(serviceProvider));

        /// <inheritdoc/>
        public Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand
        {
            if (StopExecutionOnValidation(command))
            {
                return Task.CompletedTask;
            }

            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            return handler.HandleAsync(command, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<TResult> SendAsync<TCommand, TResult>(
          TCommand command,
          CancellationToken cancellationToken = default)
          where TCommand : ICommand<TResult>
        {
            if (StopExecutionOnValidation(command))
            {
                return Task.FromResult<TResult>(default);
            }

            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
            return handler.HandleAsync(command, cancellationToken);
        }

        private bool StopExecutionOnValidation<TValidator>(TValidator command)
        {
            var validator = _serviceProvider.GetService<IValidator<TValidator>>();

            if (validator != null)
            {
                var validationResult = validator.Validate(command);

                if (!validationResult.IsValid)
                {
                    validationResult.Errors.ForEach(e => _validationContext.AddValidation(GetDisplayPropertyName(e), e.ErrorMessage));
                    return true;
                }

                if (_validationContext.HasValidations)
                {
                    return true;
                }
            }

            return false;
        }

        private static string GetDisplayPropertyName(ValidationFailure validation)
        {
            return validation.FormattedMessagePlaceholderValues.TryGetValue("PropertyName", out var propertyName) ? propertyName.ToString() : validation.PropertyName;
        }
    }
}