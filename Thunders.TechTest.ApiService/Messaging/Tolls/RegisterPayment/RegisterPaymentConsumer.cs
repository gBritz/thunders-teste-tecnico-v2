using Rebus.Handlers;
using Thunders.TechTest.ApiService.Data;

namespace Thunders.TechTest.ApiService.Messaging.Tolls.RegisterPayment;

public class RegisterPaymentConsumer(TollDbContext tollDbContext)
    : IHandleMessages<RegisterPaymentMessage>
{
    public async Task Handle(RegisterPaymentMessage message)
    {
        var payment = message.ToDomain();

        tollDbContext.Attach(payment.Plaza);
        tollDbContext.Add(payment);

        await tollDbContext.SaveChangesAsync();
    }
}