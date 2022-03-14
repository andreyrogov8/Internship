using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using MediatR;

namespace Application.Features.OfficeFeature.Commands
{
    public class CreateOfficeCommandRequest : IRequest<CreateOfficeCommandResponse>
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public bool HasFreeParking { get; set; }
    }

    public class CreateOfficeValidator : AbstractValidator<CreateOfficeCommandRequest>
    {
        public CreateOfficeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("The office name can't be empty or null!");
            RuleFor(x => x.Country).NotEmpty().WithMessage("The office country can't be empty or null!");
            RuleFor(x => x.City).NotEmpty().WithMessage("The office city can't be empty or null!");
            RuleFor(x => x.Address).NotEmpty().WithMessage("The office address can't be empty or null!");
        }
    }

    public class CreateCommandHandler : IRequestHandler<CreateOfficeCommandRequest, CreateOfficeCommandResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateOfficeCommandResponse> Handle(CreateOfficeCommandRequest request, CancellationToken cancellationToken)
        {
            var office = _mapper.Map<Office>(request);

            _context.Offices.Add(office);
            await _context.SaveChangesAsync(cancellationToken);
            return new CreateOfficeCommandResponse { Id = office.Id };
        }
    }



    public class CreateOfficeCommandResponse
    {
        public int Id { get; set; }
    }
}