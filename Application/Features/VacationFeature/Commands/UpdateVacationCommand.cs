
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.VacationFeature.Commands
{
    public class UpdateVacationCommandRequest : IRequest<UpdateVacationCommandResponse>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset VacationStart { get; set; }
        public DateTimeOffset VacationEnd { get; set; }

    }

    public class UpdateVacationCommandValidator : AbstractValidator<UpdateVacationCommandRequest>
    {
        public UpdateVacationCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId must not be blank");

        }
    }

    public class UpdateVacationgCommandHandler : IRequestHandler<UpdateVacationCommandRequest, UpdateVacationCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UpdateVacationgCommandHandler(IMapper mapper, IApplicationDbContext context, UserManager<User> userManager)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }
        public async Task<UpdateVacationCommandResponse> Handle(UpdateVacationCommandRequest request, CancellationToken cancellationToken)
        {
            var isUserExistsWithThisId = await _userManager.Users.AnyAsync(user => user.Id == request.UserId, cancellationToken);

            if (!isUserExistsWithThisId)
            {
                throw new NotFoundException($"There is no User with id={request.UserId}");
            }
            
            var vacation = await _context.Vacations.FirstOrDefaultAsync(vacation => vacation.Id == request.Id, cancellationToken);
            if (vacation == null)
            {
                throw new NotFoundException(nameof(vacation), request.Id);
            }
            vacation = _mapper.Map(request, vacation);
            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UpdateVacationCommandResponse>(vacation);

        }
    }


    public class UpdateVacationCommandResponse
    {
        public int Id { get; set; }
    }
}
