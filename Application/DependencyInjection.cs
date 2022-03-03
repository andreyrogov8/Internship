using Microsoft.Extensions.DependencyInjection;
using Application.Profiles;
using MediatR;
using System.Reflection;
using Application.Infrastructure;
using FluentValidation;
using Application.Interfaces;
using Application.TelegramBot;
using Microsoft.Extensions.Configuration;
using Application.Telegram.Commands;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddHttpClient();
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(WorkplacesProfile).Assembly));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            AssemblyScanner.FindValidatorsInAssembly(typeof(RequestValidationBehavior<,>).Assembly)
                .ForEach(result => services.AddScoped(result.InterfaceType, result.ValidatorType));

            services.AddScoped<ITelegramCommunicationService, TelegramCommunicationService>();
        }
    }
}
