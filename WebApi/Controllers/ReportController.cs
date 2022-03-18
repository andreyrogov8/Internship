using Application.Features.ReportFeature.Queries;
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

        [HttpGet("floorId")]
        public async Task<ActionResult<GetReportsByFloorResponse>> GetByFloor(
            [FromQuery] int floorId,
            DateTimeOffset startDate,
            DateTimeOffset endDate
            )
        {
            var result = await Mediator.Send(new GetReportsByFloorRequest
            {
                FloorId = floorId,
                StartDate = startDate,
                EndDate = endDate
            });
            return Ok(result);
        }

        [HttpGet("floorNumber")]
        public async Task<ActionResult<GetReportsByFloorResponse>> GetByFloorNumber(
            [FromQuery] string officeName,
            int floorNumber,
            DateTimeOffset startDate,
            DateTimeOffset endDate
            )
        {
            var result = await Mediator.Send(new GetReportsByFloorRequest
            {
                OfficeName = officeName,
                FloorNumber = floorNumber,
                StartDate = startDate,
                EndDate = endDate
            });
            return Ok(result);
        }

    }
}
