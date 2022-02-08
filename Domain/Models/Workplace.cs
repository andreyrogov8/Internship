using Domain.Enums;

namespace Domain.Models.Seats
{
    public class Workplace : BaseEntity
    {
        public int MapId { get; set; }
        public int WorkplaceNumber { get; set; }
        public WorkplaceType WorkplaceType { get; set; }
        public bool NextToWindow { get; set; }
        public bool HasPC { get; set; }
        public bool HasMonitor { get; set; }
        public bool HasKeyboard { get; set; }
        public bool HasMouse { get; set; }
        public bool HasHeadset { get; set; }

    }
}
