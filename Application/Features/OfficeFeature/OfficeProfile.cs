using Application.Features.OfficeFeature.Commands;
using Application.Features.OfficeFeature.Queries;
using AutoMapper;
using Domain.Models;
using static Application.Features.OfficeFeature.Commands.UpdateOfficeCommand;

namespace Application.Features.OfficeFeature
{
    public class OfficeProfile : Profile
    {
        public OfficeProfile()
        {
            CreateMap<Office, OfficeDto>();
            CreateMap<Office, GetOfficeByIdQueryResponse>();
            CreateMap<UpdateOfficeCommandRequest, Office>();
            CreateMap<CreateOfficeCommandRequest, Office>();
        }
    }
}
