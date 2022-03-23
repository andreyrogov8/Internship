using Application.Features.BookingFeature.Commands;
using Application.Features.MapFeature.Commands;
using Application.Features.MapFeature.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FloorsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<GetMapListQueryRequest>> GetAllAsync()
        {
            var result = await Mediator.Send(new GetMapListQueryRequest());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetMapByIdQueryResponse>> GetByIdAsync(int id)
        {
            var result = await Mediator.Send(new GetMapByIdQueryRequest { Id = id });
            return Ok(result);  
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<DeleteMapCommandResponse>> DeleteAsync(int id)
        {
            var result = await Mediator.Send(new DeleteMapCommandRequest
            {
                Id = id
            });
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<CreateMapCommandResponse>> CreateAsync([FromBody]CreateMapCommandRequest request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UpdateMapCommandResponse>> UpdateAsync(int id, [FromBody] UpdateMapCommandRequest request)
        {
            if (request.Id != id)
            {
                return BadRequest("Id's from url and from body are different");
            }
            return Ok(await Mediator.Send(request));
        }
    }
}
