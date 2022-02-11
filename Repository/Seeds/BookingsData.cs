using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeds
{
    public static class BookingsData
    {

        public static List<Booking> DefaultOffices = new List<Booking>()
        {
            new Booking()
            {
                Id = 1
            },
            new Booking()
            {
                Id = 1
            }
        };
    }
}
