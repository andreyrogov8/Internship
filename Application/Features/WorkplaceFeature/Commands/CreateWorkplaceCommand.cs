using Application.Interfaces;
using AutoMapper;
using Domain.Enums;
using Domain.Models;
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
    public class CreateWorkplaceCommandRequest : IRequest<CreateWorkplaceCommandResponse>
    {
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

    public class Validator : AbstractValidator<CreateWorkplaceCommandRequest>
    {
        public Validator()
        {
            RuleFor(x => x.WorkplaceType).IsInEnum().WithMessage("WorkplaceType must be LongTerm or ShortTerm");
            RuleFor(x => x.WorkplaceNumber).NotEmpty().WithMessage("WorkplaceNumber can not be empty or 0");
            RuleFor(x => x.MapId).NotEmpty().WithMessage("MapId can not be empty or 0");
        }
    }
    public class CreateWorkplaceCommandHandler : IRequestHandler<CreateWorkplaceCommandRequest, CreateWorkplaceCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateWorkplaceCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateWorkplaceCommandResponse> Handle(CreateWorkplaceCommandRequest request, CancellationToken cancellationToken)
        {
            var workplace = _mapper.Map<Workplace>(request);

            if (_context.Maps.AnyAsync(x => x.Id == workplace.MapId) is null)
            {
                throw new ValidationException($"There is no Map with id = {workplace.MapId}");
            }
            

            _context.Workplaces.Add(workplace);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateWorkplaceCommandResponse
            {
                Id = workplace.Id
            };
        }
    }
    public class CreateWorkplaceCommandResponse
    {
        public int Id { get; set; }
    }
}
