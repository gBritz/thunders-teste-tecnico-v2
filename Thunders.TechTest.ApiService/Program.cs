using System.Text.Json.Serialization;
using Thunders.TechTest.ApiService;
using Thunders.TechTest.ApiService.Application;
using Thunders.TechTest.ApiService.CrossCutting.Web;
using Thunders.TechTest.ApiService.Data;
using Thunders.TechTest.ApiService.Messaging.Tolls.RegisterPayment;
using Thunders.TechTest.OutOfBox.Database;
using Thunders.TechTest.OutOfBox.Queues;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services
    .AddControllers(options =>
    {
        options.ModelValidatorProviders.Clear();
        options.Filters.Add<ValidationFilter>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
builder.Services.AddEndpointsApiExplorer();

builder.Services.RegisterApplication();

var features = Features.BindFromConfiguration(builder.Configuration);

// Add services to the container.
builder.Services.AddProblemDetails();

if (features.UseMessageBroker)
{
    builder.Services.AddBus(builder.Configuration, new SubscriptionBuilder()
        .Add<RegisterPaymentConsumer>());
}

if (features.UseEntityFramework)
{
    builder.Services.AddSqlServerDbContext<TollDbContext>(builder.Configuration);
}

builder.Services.AddHostedService<EFDataSeedingHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapDefaultEndpoints();

app.MapControllers();

app.Run();