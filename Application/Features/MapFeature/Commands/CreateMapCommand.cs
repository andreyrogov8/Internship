using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.MapFeature.Commands
{
    public class CreateMapCommandRequest : IRequest<CreateMapCommandResponse>
    {
        public int FloorNumber { get; set; }
        public bool HasKitchen { get; set; }
        public bool HasMeetingRoom { get; set; }
        public int OfficeId { get; set; }
    }
    public class CreateMapCommandValidator : AbstractValidator<CreateMapCommandRequest>
    {
        public CreateMapCommandValidator()
        {
            RuleFor(x => x.FloorNumber).Must(x => x > 0).WithMessage("FloorNumber must not be negative number");
            RuleFor(x => x.HasKitchen).Must(x => x == true || x == false).WithMessage("HasKitchen should be whether true or false");
            RuleFor(x => x.HasMeetingRoom).Must(x => x == true || x == false).WithMessage("HasMeetingRoom should be whether true or false");
            RuleFor(x => x.OfficeId).NotEmpty().WithMessage("OfficeId must not be blank");

        }
    }

    public class CreateMapCommandHandler : IRequestHandler<CreateMapCommandRequest, CreateMapCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public CreateMapCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<CreateMapCommandResponse> Handle(CreateMapCommandRequest request, CancellationToken cancellationToken)
        {
            var isOfficeExistsWithThisId = await _context.Offices.AnyAsync(office => office.Id == request.OfficeId, cancellationToken);
            if (!isOfficeExistsWithThisId)
            {
                throw new NotFoundException($"There is no Office with id ={ request.OfficeId }");
            }
            var map = _mapper.Map<Map>(request);
            _context.Maps.Add(map);
            await _context.SaveChangesAsync(cancellationToken);
            return new CreateMapCommandResponse
            {
                Id = map.Id
            };
        }
    }


    public class CreateMapCommandResponse
    {
        public int Id { get; set; }
    }
}
