using Application.Features.BookingFeature.Commands;
using Application.Features.VacationFeature.Commands;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Telegram.Models
{
    public class DateInfo
    {
        public int StartDay { get; set; } 
        public int StartMonth { get; set; } 
        public int EndMonth { get; set; } 
        public int EndDay { get; set; } 
    }
    public class UserInfo
    {
        public UserState CurrentState { get; set; }
        public UserRole Role { get; set; }

        public int  MapId { get; set; }
        public CreateBookingCommandRequest Booking { get; set; } = new CreateBookingCommandRequest 
        { 
            StartDate = DateTimeOffset.UtcNow,
            EndDate = DateTimeOffset.UtcNow,
        };
        public DateInfo UserDates { get; set; } = new DateInfo();
        

        
    }
}

