using Application.Features.BookingFeature.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<GetBookingListQueryResponse>> GetAll()
        {
            var result = await _mediator.Send(new GetBookingListQueryRequest());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetBookingByIdQueryResponse>> GetById([FromRoute] GetBookingByIdQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

    }
}
