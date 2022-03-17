using Application.Features.ReportFeature.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : BaseApiController
    {
        [HttpGet("office/{office_id:int}")]
        public async Task<ActionResult<GetReportsByOfficeResponse>> GetAllAsync(
            [FromQuery] int officeId,
            DateTimeOffset startDate,
            DateTimeOffset endDate
            )
        {
            var request = new GetReportsByOfficeRequest { OfficeId = officeId, StartDate = startDate, EndDate = endDate };
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("office")]
        public async Task<ActionResult<GetReportsByCityQueryResponse>> GetByCity([FromQuery] string city)
        {
            var result = await Mediator.Send(new GetReportsByCityQueryRequest
            {
                City = city
            });
            return Ok(result);
        }

    }
}
