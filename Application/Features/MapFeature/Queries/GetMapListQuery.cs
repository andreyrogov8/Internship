using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.MapFeature.Queries
{
    public class GetMapListQueryRequest : IRequest<GetMapListQueryResponse>
    {
        public string OfficeId { get; set; }
    }
    public class GetMapListQueryHandler : IRequestHandler<GetMapListQueryRequest, GetMapListQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetMapListQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<GetMapListQueryResponse> Handle(GetMapListQueryRequest request, CancellationToken cancellationToken)
        {
            var mapList = _context.Maps.AsQueryable();
            if (request.OfficeId != null)
            {
                mapList = mapList.Where(x => x.OfficeId == Int32.Parse(request.OfficeId));
            }
            return new GetMapListQueryResponse
            {
                Results = await mapList.ProjectTo<MapDTO>(_mapper.ConfigurationProvider)
                                .ToListAsync(cancellationToken)
            };
        }
    }


    public class GetMapListQueryResponse
    {
        public IEnumerable<MapDTO> Results { get; set; }
    }

    public class MapDTO
    {
        public int Id { get; set; }
        public int FloorNumber { get; set; }
        public bool HasKitchen { get; set; }
        public bool HasMeetingRoom { get; set; }
        public int OfficeId { get; set; }
        public string Country { get; set; }
        public string City{ get; set; }
    }

}
