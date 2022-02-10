using Microsoft.Extensions.DependencyInjection;
using Application.Profiles;
using MediatR;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(WorkplacesProfile).Assembly));
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
