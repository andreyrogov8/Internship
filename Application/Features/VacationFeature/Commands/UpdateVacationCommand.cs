
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
        private readonly UserManager<User> _userManager;

        private readonly IApplicationDbContext _context;
        public UpdateVacationCommandValidator(UserManager<User> userManager, IApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;

            bool IsUserExists(int userId)
            {
                var userExists = _userManager.Users.Any(user => user.Id == userId);
                return userExists;
            }
            

            RuleFor(x => x.UserId).Must(IsUserExists).WithMessage(x => $"There is no User with id=({x.UserId})");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId must not be blank");
            RuleFor(r => r.VacationStart)
               .NotEmpty()
               .WithMessage("Vacation Date is Required");

            RuleFor(r => r.VacationEnd)
            .NotEmpty().WithMessage("End date is required")
            .GreaterThan(r => r.VacationStart)
                            .WithMessage("End date must after Start date");

        }
    }

    public class UpdateVacationgCommandHandler : UpsertVacationCommand, IRequestHandler<UpdateVacationCommandRequest, UpdateVacationCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UpdateVacationgCommandHandler(IMapper mapper, IApplicationDbContext context, UserManager<User> userManager) : base(context)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }
        public async Task<UpdateVacationCommandResponse> Handle(UpdateVacationCommandRequest request, CancellationToken cancellationToken)
        {
            
            var vacation = await _context.Vacations.FirstOrDefaultAsync(vacation => vacation.Id == request.Id, cancellationToken);
            if (vacation == null)
            {
                throw new NotFoundException(nameof(vacation), request.Id);
            }
            await EnsureUserCanUpdateThisVacationAsync(request.Id, request.UserId, request.VacationStart, request.VacationEnd, cancellationToken);
            vacation = _mapper.Map(request, vacation);
            await _context.SaveChangesAsync(cancellationToken);
            return new UpdateVacationCommandResponse { Id = vacation.Id };

        }
    }


    public class UpdateVacationCommandResponse
    {
        public int Id { get; set; }
    }
}
