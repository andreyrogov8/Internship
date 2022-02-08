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
        public string TelegramId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EmploymentStart { get; set; }
        public DateTime EmploymentEnd { get; set; }
        public int PrefferedWorkPlaceId { get; set; }
        public ICollection<Booking> Bookings { get; set; } 
        public ICollection<Vacation> Vacations { get; set; }
    }
}
