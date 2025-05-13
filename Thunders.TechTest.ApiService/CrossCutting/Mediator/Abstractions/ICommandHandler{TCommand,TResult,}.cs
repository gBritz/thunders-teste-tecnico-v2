namespace Thunders.TechTest.ApiService.CrossCutting.Mediator.Abstractions;

/// <summary>
/// Represents a handler to execute command.
/// </summary>
/// <typeparam name="TCommand">Query with data.</typeparam>
/// <typeparam name="TResult">Result of execution.</typeparam>
public interface ICommandHandler<in TCommand, TResult>
  where TCommand : ICommand<TResult>
{
    /// <summary>
    /// Handles execution of command.
    /// </summary>
    /// <param name="command">Query with data.</param>
    /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
    /// <returns>Result of execution</returns>
    Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken);
}