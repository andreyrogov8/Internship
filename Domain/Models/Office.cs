using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Office : BaseEntity
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; } = false;
        public bool HasFreeParking { get; set; }
        public ICollection<Map> Maps { get; set; }
    }
}
