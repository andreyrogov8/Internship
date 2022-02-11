using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.WorkplaceFeature.Commands
{
    public class UpdateWorkplaceCommandRequest : IRequest<UpdateWorkplaceCommandResponse>
    {
        public int Id { get; set; }
        public int WorkplaceNumber { get; set; }
        public WorkplaceType WorkplaceType { get; set; }
        public bool NextToWindow { get; set; }
        public bool HasPC { get; set; }
        public bool HasMonitor { get; set; }
        public bool HasKeyboard { get; set; }
        public bool HasMouse { get; set; }
        public bool HasHeadset { get; set; }
        public int MapId { get; set; }
    }

    public class UpdateWorkplaceCommandHandler : IRequestHandler<UpdateWorkplaceCommandRequest, UpdateWorkplaceCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateWorkplaceCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public class Validator : AbstractValidator<UpdateWorkplaceCommandRequest>
        {
            public Validator()
            {
                RuleFor(x => x.WorkplaceType).IsInEnum().WithMessage("WorkplaceType must be LongTerm or ShortTerm");
                RuleFor(x => x.WorkplaceNumber).NotEmpty().WithMessage("WorkplaceNumber can not be empty or 0");
                RuleFor(x => x.MapId).NotEmpty().WithMessage("MapId can not be empty or 0");
            }
        }
        public async Task<UpdateWorkplaceCommandResponse> Handle(UpdateWorkplaceCommandRequest request, CancellationToken cancellationToken)
        {
            var workplace = await _context.Workplaces.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (workplace == null)
            {
                throw new NotFoundException($"The workplace with the ID = {request.Id}");
            }

            if (_context.Maps.AnyAsync(x => x.Id == workplace.MapId) is null)
            {
                throw new ValidationException($"There is no Map with id = {workplace.MapId}");
            }

            workplace.WorkplaceNumber = request.WorkplaceNumber;
            workplace.WorkplaceType = request.WorkplaceType;
            workplace.NextToWindow = request.NextToWindow;
            workplace.HasPC = request.HasPC;
            workplace.HasMonitor = request.HasMonitor;
            workplace.HasKeyboard = request.HasKeyboard;
            workplace.HasMouse = request.HasMouse;
            workplace.HasHeadset = request.HasHeadset;
            workplace.MapId = request.MapId;   

            _context.Workplaces.Update(workplace);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UpdateWorkplaceCommandResponse>(workplace);
        }
    }
    public class UpdateWorkplaceCommandResponse
    {
        public int Id { get; set; }
        public int WorkplaceNumber { get; set; }
        public WorkplaceType WorkplaceType { get; set; }
        public bool NextToWindow { get; set; }
        public bool HasPC { get; set; }
        public bool HasMonitor { get; set; }
        public bool HasKeyboard { get; set; }
        public bool HasMouse { get; set; }
        public bool HasHeadset { get; set; }
        public int MapId { get; set; }
    }
}
