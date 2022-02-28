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
        NewBookingIsSelectedSelectingWorkplace,
        NewBookingIsSelectedSelectingStartDateMonth,
        NewBookingIsSelectedSelectingStartDateDay,
        NewBookingIsSelectedSelectingEndDateMonth,
        NewBookingIsSelectedelectingEndDateDay,
        
        //Checking Bookig States
        CheckingBookings,

        //Vacation States
        NewVacationIsSelected,
        NewVacationIsSelectedStartDateMonth,
        NewVacationIsSelectedStartDateDay,
        NewVacationIsSelectedEndDateMonth,
        NewVacationIsSelectedEndDateDay,



    }
}
