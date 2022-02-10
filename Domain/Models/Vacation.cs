using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Vacation : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTimeOffset VacationStart { get; set; }
        public DateTimeOffset VacationEnd { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
