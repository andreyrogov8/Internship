using Application.Features.UserFeature.Queries;
using Application.Interfaces;
using Domain.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        // ignore userService for now; it's for generating a jwt token
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles="Administrator")]
        [HttpGet]
        public async Task<ActionResult<GetUserListQueryResponse>> GetAllUsersAsync()
        {
            var users = await Mediator.Send(new GetUserListQueryRequest());
            return Ok(users);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetUserByIdQueryResponse>> GetUserByIdAsync(int id)
        {
            var user = await Mediator.Send(new GetUserByIdQueryRequest { Id = id });
            return Ok(user);
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetTokenAsync(TokenRequest request)
        {
            var result = await _userService.GetTokenAsync(request);
            return Ok(result);
        }
    }
}
