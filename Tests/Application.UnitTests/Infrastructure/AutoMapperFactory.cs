using Application.Features.MapFeature;
using Application.Features.VacationFeature;
using Application.Profiles;
using AutoMapper;

namespace Application.UnitTests.Infrastructure
{
    public static class AutoMapperFactory
    {
        public static IMapper Create()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddMaps(typeof(MapProfile),
                    typeof(WorkplacesProfile),
                    typeof(VacationProfile));
            });

            return mappingConfig.CreateMapper();
        }
    }
}
