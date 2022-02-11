using System;
using System.Collections.Generic;
using System.Linq;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using Application.Infrastructure;
using Application.Exceptions;
using FluentValidation;

namespace Application.Features.OfficeFeature.Commands
{
    public class DeleteOfficeCommand
    {
        public class DeleteOfficeCommandRequest : IRequest<DeleteOfficeCommandResponse>
        {
            public int Id { get; set; }
        }
        public class Validator : AbstractValidator<DeleteOfficeCommandRequest>
        {
            public Validator()
            {               
                RuleFor(x => x.Id).NotEmpty().WithMessage("The office Id can't be empty or null!");
            }
        }

        public class DeleteOfficeCommandHandler : IRequestHandler<DeleteOfficeCommandRequest, DeleteOfficeCommandResponse>
        {
            private readonly IApplicationDbContext _context;

            public DeleteOfficeCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<DeleteOfficeCommandResponse> Handle(DeleteOfficeCommandRequest request, CancellationToken cancellationToken)
            {
                var office = await _context.Offices.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
                if (office == null)
                {
                    throw new NotFoundException(nameof(office), request.Id);
                }
                office.IsDeleted = true;
                await _context.SaveChangesAsync(cancellationToken);

                return new DeleteOfficeCommandResponse
                {
                    Id = office.Id
                };
            }
        }
        public class DeleteOfficeCommandResponse
        {
            public int Id { get; set; }
        }
    }
}