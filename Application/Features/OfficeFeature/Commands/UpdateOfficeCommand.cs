﻿using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Features.OfficeFeature.Commands
{
    public class UpdateOfficeCommand
    {
        public class UpdateOfficeCommandRequest : IRequest<UpdateOfficeCommandResponse>
        {
          
            public int Id { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }
            public string City { get; set; }
            public string Address { get; set; }
            public bool HasFreeParking { get; set; }
        }

        public class Validator : AbstractValidator<UpdateOfficeCommandRequest>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("The office name can't be empty or null!");
            }
        }

        public class UpdateOfficeCommandHandler : IRequestHandler<UpdateOfficeCommandRequest, UpdateOfficeCommandResponse>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public UpdateOfficeCommandHandler(IApplicationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<UpdateOfficeCommandResponse> Handle(UpdateOfficeCommandRequest request, CancellationToken cancellationToken)
            {

                var office = await _context.Offices
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (office == null)
                {
                    throw new NotFoundException($"The office with the ID = {request.Id}");
                }

                office.IsDeleted = true;

                var newOffice = _mapper.Map<Office>(request);

                _context.Offices.Add(newOffice);

                await _context.SaveChangesAsync(cancellationToken);

                var response = _mapper.Map<UpdateOfficeCommandResponse>(newOffice);

                return response;
            }
        }

        public class UpdateOfficeCommandResponse
        {

            public int Id { get; set; }
            public string Name { get; set; }
            public string Country { get; set; }
            public string City { get; set; }
            public string Address { get; set; }
            public bool HasFreeParking { get; set; }
        }
    }
}
