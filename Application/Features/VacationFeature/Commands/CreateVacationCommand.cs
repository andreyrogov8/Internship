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
        public CreateVacationCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId must not be blank");
        }
    }
    public class CreateVacationCommandHandler : IRequestHandler<CreateVacationCommandRequest, CreateVacationCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public CreateVacationCommandHandler(IMapper mapper, IApplicationDbContext context, UserManager<User> userManager)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }
        public async Task<CreateVacationCommandResponse> Handle(CreateVacationCommandRequest request, CancellationToken cancellationToken)
        {

            var isUserExistsWithThisId = await _userManager.Users.AnyAsync(user => user.Id == request.UserId, cancellationToken);

            if (!isUserExistsWithThisId)
            {
                throw new NotFoundException($"There is no User with id={request.UserId}");
            }
           
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
