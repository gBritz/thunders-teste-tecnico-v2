using FluentValidation.Results;

namespace Thunders.TechTest.ApiService.CrossCutting.Validations;

/// <summary>
///   Define um contexto de validações.
/// </summary>
public class ValidationContext
{
    /// <summary>
    ///   A lista de mensagens de validação.
    /// </summary>
    private readonly List<ValidationFailure> _validationMessages = [];

    /// <summary>
    ///   Obtém a lista de mensagens de validação.
    /// </summary>
    public IReadOnlyCollection<ValidationFailure> ValidationMessages => _validationMessages;

    /// <summary>
    ///   Obtém um valor indicando se existem mensagens de validação no contexto.
    /// </summary>
    /// <value>
    ///   <c>true</c> se existem mensagens de validação no contexto; caso contrário, <c>false</c>.
    /// </value>
    public bool HasValidations => _validationMessages.Any();

    /// <summary>
    ///   Limpa a lista de mensagens de validação.
    /// </summary>
    public void Clear()
    {
        _validationMessages.Clear();
    }

    /// <summary>
    ///   Adiciona uma mensagem de validação ao contexto.
    /// </summary>
    /// <param name="propertyName">A chave.</param>
    /// <param name="message">A mensagem.</param>
    /// <returns>O contexto de validação atual.</returns>
    public ValidationContext AddValidation(string propertyName, string message)
    {
        _validationMessages.Add(new ValidationFailure(propertyName, message));
        return this;
    }

    /// <summary>
    ///   Adiciona uma mensagem de validação ao contexto.
    /// </summary>
    /// <param name="validationMessage">A mensagem de validação.</param>
    /// <returns>O contexto de validação atual.</returns>
    public ValidationContext AddValidation(ValidationFailure validationMessage)
    {
        _validationMessages.Add(validationMessage);
        return this;
    }

    /// <summary>
    ///   Adiciona uma lista de mensagens de validação ao contexto.
    /// </summary>
    /// <param name="validationMessages">A lista de mensagens de validação.</param>
    /// <returns>O contexto de validação atual.</returns>
    public ValidationContext AddValidations(params ValidationFailure[] validationMessages)
    {
        this._validationMessages.AddRange(validationMessages);
        return this;
    }
}