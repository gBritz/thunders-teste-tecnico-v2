namespace Thunders.TechTest.ApiService.CrossCutting.Mediator.Abstractions;

/// <summary>
/// Represents a handler to execute command.
/// </summary>
/// <typeparam name="TCommand">Query with data.</typeparam>
public interface ICommandHandler<in TCommand>
  where TCommand : ICommand
{
    /// <summary>
    /// Handles execution of command.
    /// </summary>
    /// <param name="command">Query with data.</param>
    /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
    Task HandleAsync(TCommand command, CancellationToken cancellationToken);
}