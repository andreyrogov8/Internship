using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeds
{
    public static class OfficesData
    {

        public static List<Office> DefaultOffices = new List<Office>()
        {

            new Office() { 
                Name = "",
                Country = "",
                City = "",
                Address = "",
                HasFreeParking = true,

            },
             new Office() {
                Name = "",
                Country = "",
                City = "",
                Address = "",
                HasFreeParking = true,

            }
        };
    }
}
