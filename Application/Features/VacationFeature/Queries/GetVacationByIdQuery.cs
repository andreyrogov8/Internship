using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.VacationFeature.Queries
{
    public class GetVacationByIdQueryRequest : IRequest<GetVacationByIdQueryResponse>
    {
        public int Id { get; set; }
    }
    public class GetVacationByIdQueryHandler : IRequestHandler<GetVacationByIdQueryRequest, GetVacationByIdQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetVacationByIdQueryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetVacationByIdQueryResponse> Handle(GetVacationByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var vacation = await _context.Vacations
                .ProjectTo<GetVacationByIdQueryResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (vacation == null)
            {
                throw new NotFoundException(nameof(vacation), request.Id);
            }
            return vacation;
        }
    }


    public class GetVacationByIdQueryResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public DateTimeOffset VacationStart { get; set; }
        public DateTimeOffset VacationEnd { get; set; }

    }
}
