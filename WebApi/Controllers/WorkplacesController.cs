using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Features.CountriesFeature.Queries;
using Application.Features.CountryCQ;
using Application.Features.WorkplaceFeature.Commands;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkplacesController : BaseApiController
    {
        //private readonly IMediator _mediator;
        //public WorkplacesController(IMediator mediator)
        //{
        //    _mediator = mediator;
        //}

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetWorkplaceListQueryRequest());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetWorkplaceByIdQueryRequest { Id = id } );
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteWorkplaceCommandRequest { Id = id });
            return Ok(result);
        }

    }
}
