namespace Domain.Models
{
    public  class Booking : BaseEntity
    {
        public int UserId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public bool IsRecurring { get; set; }
        public int Frequency { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int WorkplaceId { get; set; }
        public Workplace Workplace { get; set; }
        public User User { get; set; }
    }
}
