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
        [HttpGet("office/{officeId:int}")]
        public async Task<ActionResult<GetReportsByOfficeResponse>> GetAllAsync(
            [FromRoute] int officeId,
            DateTimeOffset startDate,
            DateTimeOffset endDate
            )
        {
            var request = new GetReportsByOfficeRequest { OfficeId = officeId, StartDate = startDate, EndDate = endDate };
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("office/{cityName}")]
        public async Task<ActionResult<GetReportsByCityQueryResponse>> GetByCity(
            [FromRoute] string cityName,
            DateTimeOffset startDate,
            DateTimeOffset endDate
            )
        {
            var result = await Mediator.Send(new GetReportsByCityQueryRequest
            {
                City = cityName,
                StartDate = startDate,
                EndDate = endDate
            });
            return Ok(result);
        }

        [HttpGet("floor/{floorId:int}")]
        public async Task<ActionResult<GetReportsByFloorResponse>> GetByFloor(
            [FromRoute] int floorId,
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

        [HttpGet("office/{officeId:int}/floor/{floorNumber}")]
        public async Task<ActionResult<GetReportsByFloorResponse>> GetByFloorNumber(
            [FromRoute] int officeId,
            [FromRoute] int floorNumber,
            DateTimeOffset startDate,
            DateTimeOffset endDate
            )
        {
            var result = await Mediator.Send(new GetReportsByFloorRequest
            {
                OfficeId = officeId,
                FloorNumber = floorNumber,
                StartDate = startDate,
                EndDate = endDate
            });
            return Ok(result);
        }

        [HttpGet("office/{officeName}/floor/{floorNumber}")]
        public async Task<ActionResult<GetReportsByFloorResponse>> GetByFloorNumber(
            [FromRoute] string officeName,
            [FromRoute] int floorNumber,
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
