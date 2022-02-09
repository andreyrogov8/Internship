using System;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Application.Features.MapFeatures.Queries
{
	public class GetAllMapQuery:IRequest<Responce>
	{


	}

    public class GetAllMapQueryHandler : IRequestHandler<GetAllMapQuery, Responce>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllMapQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Responce> Handle(GetAllMapQuery request, CancellationToken cancellationToken)
        {
            var responce =  _context.Maps.ProjectTo<MapDto>(_mapper.ConfigurationProvider).ToList();
            
            return new Responce
            {
                MapList = responce
            };
        }
    }

    public class Responce
    {
        public IEnumerable<MapDto> MapList { get; set; }
    }
	public class MapDto
    {
		public int FloorNumber { get; set; }
		public bool HasKitchen { get; set; }
		public bool HasMeetingRoom { get; set; }
		public int OfficeId { get; set; }
	}
}

