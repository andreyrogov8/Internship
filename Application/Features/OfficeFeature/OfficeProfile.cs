using Application.Features.OfficeFeature.Commands;
using Application.Features.OfficeFeature.Queries;
using AutoMapper;
using Domain.Models;

namespace Application.Features.OfficeFeature
{
    public class OfficeProfile : Profile
    {
        public OfficeProfile()
        {
            CreateMap<Office, OfficeDto>();
            CreateMap<Office, GetOfficeByIdQueryResponse>();
            CreateMap<CreateOfficeCommandRequest, Office>();
        }
    }
}
