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
        EnteringVacation,
        StartingBooking,
        CheckingBookings,
        SelectingFloor,
        SelectingWorkplace
    }
}
