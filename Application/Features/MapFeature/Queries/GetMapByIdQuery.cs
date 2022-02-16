using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.MapFeature.Queries
{
    public class GetMapByIdQueryRequest : IRequest<GetMapByIdQeryResponse>
    {
        public int Id { get; set; }
    }
    public class GetMapByIdQueryHandler : IRequestHandler<GetMapByIdQueryRequest, GetMapByIdQeryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetMapByIdQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<GetMapByIdQeryResponse> Handle(GetMapByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var map = await _context.Maps
                .ProjectTo<GetMapByIdQeryResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(map => map.Id == request.Id, cancellationToken);
            if (map == null)
            {
                throw new NotFoundException(nameof(map), request.Id);
            }
            return map;
        }
    }

    public class GetMapByIdQeryResponse
    {
        public int Id { get; set; }
        public int FloorNumber { get; set; }
        public bool HasKitchen { get; set; }
        public bool HasMeetingRoom { get; set; }
        public int OfficeId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
}
