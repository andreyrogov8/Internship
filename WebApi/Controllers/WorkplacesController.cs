using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Features.CountriesFeature.Queries;
using Application.Features.CountryCQ;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkplacesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WorkplacesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetWorkplaceListQueryRequest());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetWorkplaceByIdQueryRequest { Id = id } );
            return Ok(result);
        }

    }
}
