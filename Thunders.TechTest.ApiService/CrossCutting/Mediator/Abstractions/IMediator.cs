namespace Thunders.TechTest.ApiService.CrossCutting.Mediator.Abstractions;

/// <summary>
/// Represents mediator of commands and handlers.
/// </summary>
public interface IMediator
{
    /// <summary>
    /// Send to execute command.
    /// </summary>
    /// <typeparam name="TCommand">Command will be executed.</typeparam>
    /// <param name="command">Command with data.</param>
    /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
    Task SendAsync<TCommand>(
      TCommand command,
      CancellationToken cancellationToken = default)
      where TCommand : ICommand;

    /// <summary>
    /// Send to execute command.
    /// </summary>
    /// <typeparam name="TCommand">Command will be executed.</typeparam>
    /// <typeparam name="TResult">Result execution of command.</typeparam>
    /// <param name="command">Command with data.</param>
    /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
    /// <returns>Result of execution</returns>
    Task<TResult> SendAsync<TCommand, TResult>(
      TCommand command,
      CancellationToken cancellationToken = default)
      where TCommand : ICommand<TResult>;
}