using Microsoft.AspNetCore.Mvc;
using MediatR;
namespace WebApi.Controllers
{
    public class BaseController : Controller
    {
        public readonly IMediator _mediator;
        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
