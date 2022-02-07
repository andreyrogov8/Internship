using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Seats
{
    public class Seat : BaseEntity
    {
        public bool Status { get; set; }
        public int Number { get; set; }

        public List<Equipment> Equipments { get; set; }
        public List<SeatEquipments> SeatEquipments { get; set; }
        // public int FloorId{ get; set; }
    }
}
