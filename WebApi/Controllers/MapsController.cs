using Application.Features.MapFeature.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<GetMapListQueryRequest>> GetAllAsync()
        {
            var result = await Mediator.Send(new GetMapListQueryRequest());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetMapByIdQeryResponse>> GetByIdAsync(int id)
        {
            var result = await Mediator.Send(new GetMapByIdQueryRequest { Id = id });
            return Ok(result);  
        }
    }
}
