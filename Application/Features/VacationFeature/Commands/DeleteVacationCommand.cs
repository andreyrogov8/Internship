using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.VacationFeature.Commands
{

    public class DeleteVacationCommandRequest : IRequest<DeleteVacationCommandResponse>
    {
        public int Id { get; set; }
    }
    public class DeleteVacationCommandHandler : IRequestHandler<DeleteVacationCommandRequest, DeleteVacationCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public DeleteVacationCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<DeleteVacationCommandResponse> Handle(DeleteVacationCommandRequest request, CancellationToken cancellationToken)
        {
            var vacation = await _context.Vacations
                .FirstOrDefaultAsync(vacation => vacation.Id == request.Id, cancellationToken);
            if (vacation == null)
            {
                throw new NotFoundException(nameof(vacation), request.Id);
            }
            vacation.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return new DeleteVacationCommandResponse
            {
                Id = vacation.Id
            };
        }
    }

    public class DeleteVacationCommandResponse
    {
        public int Id { get; set; }
    }
}
