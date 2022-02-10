using Application.Exceptions;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeature.Queries
{
    public class GetUserByIdQueryRequest : IRequest<GetUserByIdQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQueryRequest, GetUserByIdQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public GetUserByIdQueryHandler(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<GetUserByIdQueryResponse> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
            if (appUser == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            var roles = await _userManager.GetRolesAsync(appUser);
            var user = _mapper.Map<User, GetUserByIdQueryResponse>(appUser);
            user.Roles = roles;

            return user;
        }
    }

    public class GetUserByIdQueryResponse
    {
        public int Id { get; set; }
        public string TelegramId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTimeOffset EmploymentStart { get; set; }
        public DateTimeOffset EmploymentEnd { get; set; }
        public int PreferredWorkplaceId { get; set; }
        public IList<string> Roles { get; set; }
    }
}
