using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum UserState
    {   
        //Common States
        ProcessNotStarted,
        StartingProcess,
        SelectingAction,
        ActionIsSelected,

        //New Booking States
        NewBookingIsSelected,
        NewBookingIsSelectedStartingBooking,
        NewBookingIsSelectedSelectingFloor,
        NewBookingIsSelectedSelectingStartDateMonth,
        NewBookingIsSelectedSelectingStartDateDay,
        NewBookingIsSelectedSelectingEndDateMonth,
        NewBookingIsSelectedelectingEndDateDay,
        NewBookingIsSelectedSelectingWorkplace,
        NewBookingIsSelectedFinishingBooking,

        //Checking Bookig States
        CheckingBookings,

        //Vacation States
        EnteringVacation,

    }
}
