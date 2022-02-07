using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Floor : BaseEntity
    {
        public int FloorNumber { get; set; }
        public bool HasKitchen { get; set; }
        public bool HasMeetingRoom { get; set; }
    }
}
