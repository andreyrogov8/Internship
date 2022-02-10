using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Application.Profiles;
using MediatR;
using System.Reflection;
using Application.Features.CountryCQ;
using Application.Features.BookingFeature;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(WorkplacesProfile).Assembly));
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(BookingProfile).Assembly));
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
