using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Seats
{
    public class Equipment : BaseEntity
    {
        public string Name{ get; set; }
        public string Type { get; set; }

        public List<Seat> Seats { get; set; } 
        public List<SeatEquipments> SeatEquipments { get; set; }

    }
}
