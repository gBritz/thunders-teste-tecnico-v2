using Thunders.TechTest.ApiService.CrossCutting.Mediator.Abstractions;
using Thunders.TechTest.ApiService.Messaging.Tolls.RegisterPayment;
using Thunders.TechTest.OutOfBox.Queues;

namespace Thunders.TechTest.ApiService.Application.Tolls.RegisterPayment;

internal class RegisterPaymentHandler(IMessageSender messageSender)
    : ICommandHandler<RegisterPaymentCommand>
{
    public async Task HandleAsync(
        RegisterPaymentCommand command,
        CancellationToken cancellationToken)
    {
        await messageSender.SendLocal(new RegisterPaymentMessage
        {
            TollPlazaId = command.TollPlazaId,
            PaidAt = command.PaidAt,
            Amount = command.Amount,
            Vehicle = command.Vehicle,
        });
    }
}