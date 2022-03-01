using Application.Features.BookingFeature.Commands;
using Application.Features.BookingFeature.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<GetBookingListQueryResponse>> GetAllAsync()
        {
            var result = await Mediator.Send(new GetBookingListQueryRequest());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetBookingByIdQueryResponse>> GetByIdAsync(int id)
        {
            var result = await Mediator.Send(new GetBookingByIdQueryRequest { Id = id});
            return Ok(result);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<DeleteBookingCommandResponse>> DeleteAsync(int id)
        {
            var result = await Mediator.Send(new DeleteBookingCommandRequest { Id = id });
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CreateBookingCommandResponse>> PostAsync([FromBody] CreateBookingCommandRequest request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<UpdateBookingCommandResponse>> Update(int id, [FromBody] UpdateBookingCommandRequest request)
        {
            if (request.UserId != id)
            {
                return BadRequest("Id's from url and from body are different");
            }
            return Ok(await Mediator.Send(request));
        }

    }
}
