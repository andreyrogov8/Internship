using Application.Features.BookingFeature.Queries;
using Application.Features.VacationFeature.Commands;
using Application.Features.VacationFeature.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VacationsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<GetVacationListQueryResponse>> GetAllAsync()
        {
            var result = await Mediator.Send(new GetVacationListQueryRequest());
            return Ok(result);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetVacationByIdQueryResponse>> GetByIdAsync(int id)
        {
            var result = await Mediator.Send(new GetVacationByIdQueryRequest
            {
                Id = id
            });
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<DeleteVacationCommandResponse>> DeleteAsync(int id)
        {
            var result = await Mediator.Send(new DeleteVacationCommandRequest { Id = id });
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<CreateVacationCommandResponse>> PostAsync([FromBody] CreateVacationCommandRequest request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<UpdateVacationCommandResponse>> Update(int id, [FromBody] UpdateVacationCommandRequest request)
        {
            if (request.Id != id)
            {
                return BadRequest("Id's from url and from body are different");
            }
            return Ok(await Mediator.Send(request));
        }
    }
}
