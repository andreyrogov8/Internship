using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Features.CountriesFeature.Queries;
using Application.Features.CountryCQ;
using Application.Features.WorkplaceFeature.Commands;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkplacesController : BaseApiController
    {


        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Request request)
        {
            var _request = new GetWorkplaceListQueryRequest
            {
                MapId = request.MapId,
                HasWindow = request.HasWindow,
                HasPc = request.HasPc,
                HasMonitor = request.HasMonitor,
                HasKeyboard = request.HasKeyboard,
                HasMouse = request.HasMouse,
                HasHeadset = request.HasHeadset
            };
            var result = await Mediator.Send(_request);
            return Ok(result);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetWorkplaceByIdQueryRequest { Id = id } );
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CreateWorkplaceCommandResponse>> Create([FromBody] CreateWorkplaceCommandRequest request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,[FromBody] UpdateWorkplaceCommandRequest request)
        {
            if (request.Id != id)
            {
                return BadRequest("Id's from url and from body are different");
            }
            return Ok(await Mediator.Send(request));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteWorkplaceCommandRequest { Id = id });
            return Ok(result);
        }

    }
}
