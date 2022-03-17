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
        public int StartYear { get; set; }
        public int StartMonth { get; set; }
        public int StartDay { get; set; } 
   
        public int EndYear { get; set; }
        public int EndMonth { get; set; } 
        public int EndDay { get; set; } 
    }
    public class UserInfo
    {
        public UserState CurrentState { get; set; }
        public UserRole Role { get; set; }
        public int UserId { get; set; }
        public int MapId { get; set; }
        public int WorkplaceId { get; set; }
        public DateInfo UserDates { get; set; } = new DateInfo();

        public bool? HasWindow { get; set; }
        public bool? HasPc { get; set; }
        public bool? HasMonitor { get; set; }
        public bool? HasKeyboard { get; set; }
        public bool? HasMouse { get; set; }
        public bool? HasHeadset { get; set; }

        public string RecurringDay { get; set; }
        public bool RecurringDayWasNotFound { get; set; }

        public int SelectedBookingId { get; set; }

        public DateTimeOffset StartedActionDateTime { get; set; }
        public DateTimeOffset CurrentDateTime { get; set; }
    }
}

