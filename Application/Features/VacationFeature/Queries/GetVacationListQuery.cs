using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.VacationFeature.Queries
{
    public class GetVacationListQueryRequest : IRequest<GetVacationListQueryResponse>
    {
        public long? TelegramId { get; set; }
    }

    public class GetVacationListQueryHandler : IRequestHandler<GetVacationListQueryRequest, GetVacationListQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetVacationListQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<GetVacationListQueryResponse> Handle(GetVacationListQueryRequest request, CancellationToken cancellationToken)
        {
            var vacations = _context.Vacations.AsQueryable();
            if (request.TelegramId.HasValue)
            {
                vacations = vacations.Where(v => v.User.TelegramId == request.TelegramId);
            }

            return new GetVacationListQueryResponse
            {
                Results = await vacations.
                          ProjectTo<VacationDTO>(_mapper.ConfigurationProvider)
                          .ToListAsync(cancellationToken)
            };
        }
    }


    public class GetVacationListQueryResponse
    {
       public IEnumerable<VacationDTO> Results { get; set; }
    }

    public class VacationDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset VacationStart { get; set; }
        public DateTimeOffset VacationEnd { get; set; }
    }
}
