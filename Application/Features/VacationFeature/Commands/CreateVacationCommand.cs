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
    public class CreateVacationCommandRequest : IRequest<CreateVacationCommandResponse>
    {
        public int UserId { get; set; }
        public DateTimeOffset VacationStart { get; set; }
        public DateTimeOffset VacationEnd { get; set; }
    }

    public class CreateVacationCommandValidator : AbstractValidator<CreateVacationCommandRequest>
    {
        private readonly UserManager<User> _userManager;

        public CreateVacationCommandValidator(UserManager<User>userManager)
        {
            _userManager = userManager;

            bool IsUserExists(int userId)
            {
                var userExists = _userManager.Users.Any(user => user.Id == userId);
                return userExists;
            }
            RuleFor(x => x.UserId).Must(IsUserExists).WithMessage(x => $"There is no User with id=({x.UserId})");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId must not be blank");
            RuleFor(r => r.VacationStart)
               .NotEmpty()
               .WithMessage("Vacation Start Date is Required");

            RuleFor(r => r.VacationEnd)
            .NotEmpty().WithMessage("End date is required")
            .GreaterThan(r => r.VacationStart)
                            .WithMessage("End date must after Start date");
        }
    }
    public class CreateVacationCommandHandler : UpsertVacationCommand, IRequestHandler<CreateVacationCommandRequest, CreateVacationCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public CreateVacationCommandHandler(IMapper mapper, IApplicationDbContext context, UserManager<User> userManager) : base(context)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }
        public async Task<CreateVacationCommandResponse> Handle(CreateVacationCommandRequest request, CancellationToken cancellationToken)
        {
            await EnsureTheUserHasNotVacationInThisTimeAsync(request.UserId, request.VacationStart, request.VacationEnd, cancellationToken);
            
            var vacation = _mapper.Map<Vacation>(request);
            _context.Vacations.Add(vacation);
            await _context.SaveChangesAsync(cancellationToken);
            return new CreateVacationCommandResponse { Id = vacation.Id };
        }
    }

    public class CreateVacationCommandResponse
    {
        public int Id { get; set; }
    }
}
