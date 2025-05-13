namespace Thunders.TechTest.ApiService.CrossCutting.Mediator.Abstractions;

/// <summary>
/// Represents command to execute.
/// </summary>
/// <typeparam name="TResult">Type to result of command.</typeparam>
public interface ICommand<out TResult> : ICommand
{
}