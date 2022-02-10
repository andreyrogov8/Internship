using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Features.CountriesFeature.Queries;
using Application.Features;
using Application.Features.OfficeFeature.Queries;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : Controller
    {
        private readonly IMediator _mediator;
        public OfficeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetOfficeListQueryRequest());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetOfficeByIdQueryRequest { Id = id });
            return Ok(result);
        }

    }
}
