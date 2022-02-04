using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Office : BaseEntity
    {
        public string OfficeName { get; set; }
        public int NumFloors { get; set; }
        public string Address { get; set; }
        public bool IsFreeParkingAvailable { get; set; }
    }
}
