using MediatR;
using Microsoft.AspNetCore.Mvc;

// all controllers will inherit from ApiControllerBase
namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender _mediator = null;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
