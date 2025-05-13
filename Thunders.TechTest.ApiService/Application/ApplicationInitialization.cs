using System.Reflection;
using FluentValidation;
using Infrastructure.Domain.Mediator;
using Thunders.TechTest.ApiService.CrossCutting.Extensions;
using Thunders.TechTest.ApiService.CrossCutting.Mediator.Abstractions;
using Thunders.TechTest.ApiService.CrossCutting.Validations;
using Thunders.TechTest.OutOfBox.Queues;

namespace Thunders.TechTest.ApiService.Application;

internal static class ApplicationInitialization
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();

        services
            .AddScoped<IMediator, ServiceProviderMediator>()
            .RegisterCommandsFromAssemblies(currentAssembly)
            .RegisterValidatorsFromAssemblies(currentAssembly);

        services.AddScoped<ValidationContext>();

        services.AddSingleton<IMessageSender, RebusMessageSender>();

        return services;
    }

    /// <summary>
    /// Register commands from assseblies.
    /// </summary>
    /// <param name="assemblies">Assemblies to register dynamically.</param>
    /// <returns>Configuration of mediator</returns>
    private static IServiceCollection RegisterCommandsFromAssemblies(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(assemblies, nameof(assemblies));

        var handlerType = typeof(ICommandHandler<>);
        var handlerWithResultType = typeof(ICommandHandler<,>);

        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes()
              .Where(t => t.IsClass)
              .Select(t => new
              {
                  Contract = t.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition().In(handlerType, handlerWithResultType)),
                  Implementation = t,
              })
              .Where(_ => _.Contract != null);

            foreach (var type in types)
            {
                services.AddTransient(type.Contract, type.Implementation);
            }
        }

        return services;
    }

    /// <summary>
    /// Register validators from assemblies.
    /// </summary>
    /// <param name="assemblies">Assemblies to register dynamically.</param>
    /// <returns>Configuration of mediator</returns>
    private static IServiceCollection RegisterValidatorsFromAssemblies(
        this IServiceCollection services,
        params Assembly[] assemblies)
    {
        ArgumentNullException.ThrowIfNull(assemblies, nameof(assemblies));

        AssemblyScanner.FindValidatorsInAssemblies(assemblies, includeInternalTypes: true)
          .ForEach(_ => services.AddScoped(_.InterfaceType, _.ValidatorType));

        return services;
    }
}