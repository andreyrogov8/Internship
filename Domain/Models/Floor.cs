using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Floor : BaseEntity
    {
        public int NumPlaces { get; set; }
        public int FloorNum { get; set; }
        public bool IsKitchenPresent { get; set; }
        public bool IsMeetingRoomPresent { get; set; }
    }
}
