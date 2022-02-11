﻿using Application.Features.BookingFeature.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : BaseApiController
    {       

        [HttpGet]
        public async Task<ActionResult<GetBookingListQueryResponse>> GetAll()
        {
            var result = await Mediator.Send(new GetBookingListQueryRequest());
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetBookingByIdQueryResponse>> GetById(int id)
        {
            var result = await Mediator.Send(new GetBookingByIdQueryRequest { Id = id});
            return Ok(result);
        }

    }
}