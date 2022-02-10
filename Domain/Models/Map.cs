using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Map : BaseEntity
    {
        public int FloorNumber { get; set; }
        public bool HasKitchen { get; set; }
        public bool HasMeetingRoom { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int OfficeId { get; set; }
        public Office Office { get; set; }
        public ICollection<Workplace> Workplaces { get; set; }
    }
}
