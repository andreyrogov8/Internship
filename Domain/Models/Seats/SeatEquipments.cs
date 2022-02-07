using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Seats
{
    public class SeatEquipments : BaseEntity
    {
        public int SeatId { get; set; }
        public int  EquipmentId { get; set; }

        public Seat Seat { get; set; }
        public Equipment Equipment { get; set; }


    }
}
