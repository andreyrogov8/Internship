using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Features.CountriesFeature.Queries;
using Application.Features.CountryCQ;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkplaceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WorkplaceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetAllWorkplaceList")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllWorkplaceListQueryRequest());
            return Ok(result);
        }

    }
}
