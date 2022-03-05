﻿using System;
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

        // New Booking by location
        EnteringLocation,

        //New Booking States
        NewBookingIsSelected,
        NewBookingIsSelectedStartingBooking,
        NewBookingIsSelectedSelectingFloor,
        NewBookingIsSelectedSelectingStartDateYear,
        NewBookingIsSelectedSelectingStartDateMonth,
        NewBookingIsSelectedSelectingStartDateDay,
        NewBookingIsSelectedSelectingEndDateYear,
        NewBookingIsSelectedSelectingEndDateMonth,
        NewBookingIsSelectedSelectingEndDateDay,
        NewBookingIsSelectedSelectingBookingType,
        NewBookingIsSelectedBookingTypeIsSelected,
        NewBookingIsSelectedSelectingWorkplaceByStandartBooking,
        NewBookingIsSelectedSelectingWindowOption,
        NewBookingIsSelectedSelectingPcOption,
        NewBookingIsSelectedSelectingMonitorOption,
        NewBookingIsSelectedSelectingKeyboardOption,
        NewBookingIsSelectedSelectingMouseOption,
        NewBookingIsSelectedSelectingHeadseetOption,
        NewBookingIsSelectedSelectingWorkplace,
        NewBookingIsSelectedFinishingBooking,

        //Checking Bookig States
        CheckingBookings,

        // Checking vacations
        CheckingVacations,

        //Vacation States
        NewVacationIsSelected,
        NewVacationIsSelectedStartDateMonth,
        NewVacationIsSelectedStartDateDay,
        NewVacationIsSelectedEndDateMonth,
        NewVacationIsSelectedEndDateDay,



    }
}
