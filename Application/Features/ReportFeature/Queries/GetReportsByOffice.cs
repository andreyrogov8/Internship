using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ReportFeature.Queries;

    public class GetReportsByOfficeRequest : IRequest<GetReportsByOfficeResponse>
    {
        public int OfficeId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }

    public class GetReportsByOfficeHandler : IRequestHandler<GetReportsByOfficeRequest, GetReportsByOfficeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public GetReportsByOfficeHandler(IMapper mapper, IApplicationDbContext context, UserManager<User> userManager)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

    public async Task<GetReportsByOfficeResponse> Handle(GetReportsByOfficeRequest request, CancellationToken cancellationToken)
    {
        var bookingList = _context.Bookings.AsQueryable();

        if (request.OfficeId > 0)
        {
            bookingList = bookingList.Where(b =>
               b.Workplace.Map.OfficeId == request.OfficeId);
        }

        if (bookingList.Any() && request.StartDate > DateTimeOffset.MinValue && request.EndDate > DateTimeOffset.MinValue)
        {
            bookingList = bookingList.Where(b =>
                  b.StartDate.Date >= request.StartDate.Date &&
                  b.EndDate.Date <= request.EndDate.Date);
        }

        return new GetReportsByOfficeResponse
            {
                Data = await bookingList
                        .ProjectTo<BookingDTO>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken)
            };
        }
    }


    public class GetReportsByOfficeResponse
    {
        public IEnumerable<BookingDTO> Data { get; set; }
    }

    public class BookingDTO
    {
        public int Id { get; set; }
        public int MapId { get; set; }
        public string City { get; set; }
        public string Office { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool IsRecurring { get; set; }
        public UserDto User { get; set; }
        public int WorkplaceId { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public long TelegramId { get; set; }
        public UserRole Role { get; set; }
    }
