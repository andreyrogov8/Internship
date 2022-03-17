using Application.Features.OfficeFeature.Commands;
using Application.Features.OfficeFeature.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Application.Features.OfficeFeature.Commands.UpdateOfficeCommand;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class OfficesController : BaseApiController
    {

        [HttpPost]
        public async Task<ActionResult<CreateOfficeCommandResponse>> CreateAsync([FromBody]CreateOfficeCommandRequest request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UpdateOfficeCommandResponse>> UpdateAsync([FromBody] UpdateOfficeCommandRequest request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetOfficeListQueryRequest request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetOfficeByIdQueryRequest { Id = id });
            return Ok(result);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteOfficeCommandRequest { Id = id });
            return Ok(result);
        }

    }
}
