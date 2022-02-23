using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public class GetUserListQueryRequest : IRequest<GetUserListQueryResponse>
    {
    }

    public class GetUserListQueryHandler : IRequestHandler<GetUserListQueryRequest, GetUserListQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public GetUserListQueryHandler(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<GetUserListQueryResponse> Handle(GetUserListQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users
                .ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return new GetUserListQueryResponse
            {
                Users = users
            };
        }
    }
    public class GetUserListQueryResponse
    {
        public IEnumerable<UserDTO> Users { get; set; }
    }

    public class UserDTO
    {
        public int Id { get; set; }
        public long TelegramId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTimeOffset EmploymentStart { get; set; }
        public DateTimeOffset EmploymentEnd { get; set; }
        public int PreferredWorkplaceId { get; set; }
    }
}
