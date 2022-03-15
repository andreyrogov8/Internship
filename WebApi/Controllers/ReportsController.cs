using Application.Features.ReportFeature.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : BaseApiController
    {

        [HttpGet("city/{id:int}")]
        public async Task<ActionResult<GetReportsByCityQueryResponse>> GetByCity(int id, [FromQuery] GetReportsByCityFilter filters)
        {
            var result = await Mediator.Send(new GetReportsByCityQueryRequest
            {
                Id = id,
                StartDate = DateTimeOffset.Parse(filters.StartDate),
                EndDate = DateTimeOffset.Parse(filters.EndDate),
            });
            return Ok(result);
        }

    }
}
