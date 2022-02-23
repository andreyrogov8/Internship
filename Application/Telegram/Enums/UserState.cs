using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum UserState
    {
        ProcessNotStarted,
        StartingProcess,
        SelectingAction,
        ActionIsSelected,
//<<<<<<< HEAD
        EnteringVacation,
//=======
        BookingIsSelected,
//>>>>>>> 74d1b966129b914500759bc880d2dfae938ae129
        StartingBooking,
        CheckingBookings,
        SelectingFloor,
        SelectingWorkplace
    }
}
