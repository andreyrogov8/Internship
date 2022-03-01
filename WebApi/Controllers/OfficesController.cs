using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Features.CountriesFeature.Queries;
using Application.Features;
using Application.Features.OfficeFeature.Queries;
using static Application.Features.OfficeFeature.Commands.DeleteOfficeCommand;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class OfficesController : BaseApiController
    {        

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetOfficeListQueryRequest());
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
