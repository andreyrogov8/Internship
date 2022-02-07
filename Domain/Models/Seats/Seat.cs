﻿using Domain.Enums;

namespace Domain.Models.Seats
{
    public class Seat : BaseEntity
    {
        public bool Status { get; set; }
        public int Number { get; set; }
        public SeatType SeatType { get; set; }

        public List<SeatEquipments> SeatEquipments { get; set; }
    }
}
