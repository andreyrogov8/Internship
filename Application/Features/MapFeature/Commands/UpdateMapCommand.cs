using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.BookingFeature.Commands
{
    public class UpdateMapCommandRequest : IRequest<UpdateMapCommandResponse>
    {
        public int Id { get; set; }
        public int FloorNumber { get; set; }
        public bool HasKitchen { get; set; }
        public bool HasMeetingRoom { get; set; }
        public int OfficeId { get; set; }

    }

    public class UpdateMapCommandValidator : AbstractValidator<UpdateMapCommandRequest>
    {
        public UpdateMapCommandValidator()
        {
            RuleFor(map => map.FloorNumber).NotEmpty().WithMessage("FloorNumber must not be blank");
            RuleFor(x => x.HasKitchen).Must(x => x == true || x == false).WithMessage("HasKitchen should be whether true or false");
            RuleFor(x => x.HasMeetingRoom).Must(x => x == true || x == false).WithMessage("HasMeetingRoom should be whether true or false");
            RuleFor(x => x.OfficeId).NotEmpty().WithMessage("OfficeId must not be blank");

        }
    }

    public class UpdateMapCommandHandler : IRequestHandler<UpdateMapCommandRequest, UpdateMapCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public UpdateMapCommandHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<UpdateMapCommandResponse> Handle(UpdateMapCommandRequest request, CancellationToken cancellationToken)
        {
           var isOfficeExistsWithThisId = await _context.Maps.AnyAsync(map => map.Id == request.Id, cancellationToken);
            if (!isOfficeExistsWithThisId)
            {
                throw new NotFoundException($"There is no Office with id={request.OfficeId}");
            }
            var map = await _context.Maps.FirstOrDefaultAsync(map => map.Id == request.Id, cancellationToken);
            if (map == null)
            {
                throw new NotFoundException(nameof(map), request.Id);
            }
            map = _mapper.Map(request, map);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UpdateMapCommandResponse>(map);
        }
    }


    public class UpdateMapCommandResponse
    {
        public int Id { get; set; }
    }
}
