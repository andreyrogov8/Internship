using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.MapFeature.Queries
{
    public class GetMapByIdQueryRequest : IRequest<GetMapByIdQueryResponse>
    {
        public int Id { get; set; }
    }
    public class GetMapByIdQueryHandler : IRequestHandler<GetMapByIdQueryRequest, GetMapByIdQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetMapByIdQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<GetMapByIdQueryResponse> Handle(GetMapByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var map = await _context.Maps
                .ProjectTo<GetMapByIdQueryResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(map => map.Id == request.Id, cancellationToken);
            if (map == null)
            {
                throw new NotFoundException(nameof(map), request.Id);
            }
            return map;
        }
    }

    public class GetMapByIdQueryResponse
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
