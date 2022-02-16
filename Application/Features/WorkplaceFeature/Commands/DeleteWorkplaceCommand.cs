using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.WorkplaceFeature.Commands
{
    public class DeleteWorkplaceCommandRequest : IRequest<DeleteWorkplaceCommandResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteWorkplaceCommandHandler : IRequestHandler<DeleteWorkplaceCommandRequest, DeleteWorkplaceCommandResponse>
    {
        private readonly IApplicationDbContext _context;

        public DeleteWorkplaceCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DeleteWorkplaceCommandResponse> Handle(DeleteWorkplaceCommandRequest request, CancellationToken cancellationToken)
        {
            var workplace = await _context.Workplaces.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (workplace == null)
            {
                throw new NotFoundException($"Workplace with Id = {workplace.Id}");
            }
            workplace.IsDeleted = true;
            await _context.SaveChangesAsync(cancellationToken);

            return new DeleteWorkplaceCommandResponse
            {
                Id = workplace.Id
            };
        }
    }
    public class DeleteWorkplaceCommandResponse
    {
        public int Id { get; set; }
    }
}
