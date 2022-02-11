using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Features.OfficeFeature.Commands
{
    public class CreateOfficeCommand
    {
        public class CreateOfficeCommandRequest : IRequest<CreateOfficeCommandResponse>
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }
            public string City { get; set; }
            public string Address { get; set; }
            public bool HasFreeParking { get; set; }
        }

        public class Validator : AbstractValidator<CreateOfficeCommandRequest>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("The office name can't be empty or null!");
            }
        }

        public class Handler : IRequestHandler<CreateOfficeCommandRequest, CreateOfficeCommandResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
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
}