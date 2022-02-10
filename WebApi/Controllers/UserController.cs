using Application.Features.UserFeature.Queries;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseApiController
    {
        // ignore userService for now; it's for generating a jwt token
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<GetUserListQueryResponse>> GetAllUsersAsync()
        {
            var users = await Mediator.Send(new GetUserListQueryRequest());
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetUserByIdQueryResponse>> GetUserByIdAsync(int id)
        {
            var user = await Mediator.Send(new GetUserByIdQueryRequest { Id = id });
            return Ok(user);
        }
    }
}
