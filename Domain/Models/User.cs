using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User : IdentityUser<int>
    {
        public long TelegramId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset EmploymentStart { get; set; }
        public DateTimeOffset EmploymentEnd { get; set; }
        public int PrefferedWorkPlaceId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<Booking> Bookings { get; set; } 
        public ICollection<Vacation> Vacations { get; set; }
    }
}
