using Application.Features.BookingFeature.Commands;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Telegram.Models
{
    public class UserInfo
    {
        public UserState CurrentState { get; set; }
        public UserRole Role { get; set; }

        public int  MapId { get; set; }
        public CreateBookingCommandRequest Booking { get; set; } 
    }
}

