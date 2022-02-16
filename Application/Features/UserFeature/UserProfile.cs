using Application.Features.UserFeature.Queries;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeature
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, GetUserByIdQueryResponse>();
            CreateMap<User, UserDTO>();
        }
    }
}
