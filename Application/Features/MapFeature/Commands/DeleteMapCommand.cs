using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.MapFeature.Commands
{
    public class DeleteMapCommandRequest : IRequest<DeleteMapCommandResponse>
    {
        public int Id { get; set; }
    }
    public class DeleteMapCommandHandler : IRequestHandler<DeleteMapCommandRequest, DeleteMapCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public DeleteMapCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<DeleteMapCommandResponse> Handle(DeleteMapCommandRequest request, CancellationToken cancellationToken)
        {
            var map = await _context.Maps.FirstOrDefaultAsync(map => map.Id == request.Id, cancellationToken);
            if (map == null)
            {
                throw new NotFoundException(nameof(map), request.Id);
            }
            map.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);
            return new DeleteMapCommandResponse
            {
                Id = map.Id
            };
        }
    }


    public class DeleteMapCommandResponse
    {
        public int Id { get; set; }
    }
}
